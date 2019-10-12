using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class SampledTrail : ITrail
    {
        private List<Vector3> Samples = null;
        public SampledTrail(IEnumerable<Vector3> samples)
        {
            this.Samples = new List<Vector3>(samples);
        }
        public List<Vector3> GetSampledLocations()
        {
            return this.Samples;
        }
    }
}
