using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Sandbox
{
    public class SampledTrail : ITrail
    {
        private readonly List<Vector3> samples;

        public SampledTrail(IEnumerable<Vector3> samples)
        {
            this.samples = new List<Vector3>(samples);
        }
        public IEnumerable<Vector3> GetSampledLocations()
        {
            return this.samples;
        }
    }
}
