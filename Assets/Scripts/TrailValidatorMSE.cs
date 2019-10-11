using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class TrailValidatorMSE : ITrailValidator
    {
        private float threshold = Mathf.Infinity;
        public TrailValidatorMSE(float threshold)
        {
            this.threshold = threshold;
        }

        public bool Validate(ITrail expected, ITrail actual)
        {
            List<Vector3> expectedSamples = expected.getSampledLocations();
            List<Vector3> actualSamples = expected.getSampledLocations();

            var diff = expectedSamples.Zip(actualSamples, (e, a) => e-a);
            var normDiff = diff.Select(d => d.magnitude);
            var sumNormDiff = normDiff.Aggregate(0.0, (x, y) => x + y);
            var mse = sumNormDiff / diff.Count();
            bool isValid = mse < threshold;
            return isValid;
        }
    }
}
