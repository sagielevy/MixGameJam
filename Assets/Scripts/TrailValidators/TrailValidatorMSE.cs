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

        public bool Validate(ITrail expected, ITrail actual, float threshold)
        {
            var expectedSamples = expected.GetSampledLocations();
            var actualSamples = actual.GetSampledLocations();

            int count = expectedSamples.Count();
            for (int i = 0; i < count / 2; i++)
            {
                var diff = expectedSamples.Skip(i).Zip(actualSamples.Take(count-i), (e, a) => e - a);
                var normDiff = diff.Select(d => d.magnitude);
                var sumNormDiff = normDiff.Aggregate(0.0, (x, y) => x + y);
                var mse = sumNormDiff / diff.Count();
                bool isValid = mse <= threshold;
                Debug.Log($"mse: {mse}");
                if (isValid)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
