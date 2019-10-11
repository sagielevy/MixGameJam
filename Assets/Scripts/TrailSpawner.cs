using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{ 
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailSpawner : MonoBehaviour, ITrail
    {
        [SerializeField] private float sampleIntervalTimeSeconds = 0.1f;

        private TrailRenderer trailRenderer;
        private float lastSampleTime;
        private List<Vector3> samples;

        private void Start()
        {
            trailRenderer = GetComponent<TrailRenderer>();
            samples = new List<Vector3>();
        }

        public void Configure(float animateTimeSeconds)
        {
            trailRenderer.time = animateTimeSeconds;
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (Time.time - lastSampleTime >= sampleIntervalTimeSeconds)
            {
                lastSampleTime = Time.time;
                samples.Add(transform.position);
            }
        }
    }
}
