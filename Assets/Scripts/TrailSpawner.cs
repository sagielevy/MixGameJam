using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{ 
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailSpawner : MonoBehaviour, ITrail
    {
        [SerializeField] private FloatReference sampleIntervalTimeSeconds;
        [SerializeField] private FloatReference animateTimeSeconds;

        private TrailRenderer trailRenderer;
        private float lastSampleTime;
        private List<Vector3> samples;

        private void Start()
        {
            samples = new List<Vector3>();
            trailRenderer = GetComponent<TrailRenderer>();
            trailRenderer.time = animateTimeSeconds.GetValue();
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (Time.time - lastSampleTime >= sampleIntervalTimeSeconds.GetValue())
            {
                lastSampleTime = Time.time;
                samples.Add(transform.position);
            }
        }
    }
}
