using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class SolutionTrailGenerator : ITrail
    {
        private readonly List<Vector3> samples;

        public SolutionTrailGenerator()
        {
            samples = new List<Vector3>();
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }
    }
}
