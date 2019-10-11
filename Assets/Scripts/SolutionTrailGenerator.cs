using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{
    public class SolutionTrailGenerator : MonoBehaviour, ITrail
    {
        [SerializeField] private FloatReference speedFactor;
        [SerializeField] private float simulationSpeed;

        private List<Vector3> samples;
        private float startTime;

        private void Start()
        {
            samples = new List<Vector3>();
            speedFactor.SetValue(simulationSpeed);
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }
    }
}
