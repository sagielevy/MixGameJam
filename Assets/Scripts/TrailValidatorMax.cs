using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class TrailValidatorMax : ITrailValidator
    {
        private float threshold = Mathf.Infinity;
        public TrailValidatorMax(float threshold)
        {
            this.threshold = threshold;
        }

        public bool Validate(ITrail expected, ITrail actual)
        {
            List<Vector3> expectedSamples = expected.getSampledLocations();
            List<Vector3> actualSamples = expected.getSampledLocations();

            var diff = expectedSamples.Zip(actualSamples, (e, a) => e-a);
            var normDiff = diff.Select(d => d.magnitude);
            var maxNormDiff = normDiff.Aggregate(Mathf.NegativeInfinity, (x, y) => Mathf.Max(x,y));

            bool isValid = maxNormDiff < threshold;
            return isValid;
        }
    }
}
