using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Numpy;
using Python.Runtime;

namespace Assets.Scripts
{
    class TrailValidatorSpline : ITrailValidator
    {
         public bool Validate(ITrail expected, ITrail actual, float threshold)
        {
            var expectedSamples = expected.GetSampledLocations();
            var actualSamples = actual.GetSampledLocations();

            var (np_exp, np_act) = prepare_np_input(expectedSamples, actualSamples);

            // we know that the curves are sampled at same time intervals. If we didn't we would need to interpolate
            // both of them (cubic spline or other) and sample at equal times or referably at equal distances.
            // Since we do know, we can skip this phase.
            // Resampling at same-spacial-distances could help, but it would be negligible here.

            //normalize both curves to zero mean to be able to find curves that look the same but are far away
            var one = new int[1]; one[0] = 1;
           
            var exp_mean = np.mean(np_exp, one);
            var exp_normalized = np_exp - exp_mean;
            var act_mean = np.mean(np_act, one);
            var act_normalized = np_act - act_mean;

            // now use rigid body transform optimization, to find the matrix that registers both curves.
            // this can be done because we have ordering on all points.
            // http://nghiaho.com/?page_id=671

            var H = np.dot(exp_normalized, act_normalized.T);
            var (U, S, V) = np.linalg.svd(H);
            if ((double)np.linalg.det(V) < 0)
            {
                V[":, 2"] = -V[":, 2"];
            }
            var R_exp_to_act = np.dot(V, U.T);

            //apply rotation on exp and calculate mse
            var exp_rotated = np.dot(R_exp_to_act, exp_normalized);
            var error = np.linalg.norm(exp_rotated - act_normalized);

            return error < threshold;
        }

        private static (Numpy.NDarray<double>, Numpy.NDarray<double>) prepare_np_input(IEnumerable<Vector3> expectedSamples, IEnumerable<Vector3> actualSamples)
        {
            double[,] dexp = new double[expectedSamples.Count(), 3];
            double[,] dact = new double[actualSamples.Count(), 3];
            for (var i = 0; i < expectedSamples.Count(); ++i)
            {
                for (var j = 0; j < 3; ++j)
                {
                    dexp[i, j] = expectedSamples.ElementAt(i)[j];
                    dact[i, j] = actualSamples.ElementAt(i)[j];
                }
            }
            var np_exp = np.array(dexp);
            var np_act = np.array(dact);

            return (np_exp, np_act);
        }
    }
}
