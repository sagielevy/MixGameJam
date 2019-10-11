using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{
    public class SolutionTrailGenerator : MonoBehaviour, ITrail
    {
        [SerializeField] private FloatReference sampleIntervalSeconds;
        [SerializeField] private FloatReference animateTimeSeconds;
        [SerializeField] private FloatReference speedFactor;
        [SerializeField] private float simulationSpeed;

        private List<Vector3> samples;
        private float startTime;
        private float lastRealInterval;
        private float realInterval;
        private float realAnimateTime;

        private void Start()
        {
            samples = new List<Vector3>();
            speedFactor.SetValue(simulationSpeed);
            realInterval = sampleIntervalSeconds.GetValue() / speedFactor.GetValue();
            realAnimateTime = animateTimeSeconds.GetValue() / speedFactor.GetValue();
            startTime = Time.time;
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (Time.time - startTime >= realAnimateTime)
            {
                StopSimulation();
                return;
            }

            if (Time.time - lastRealInterval >= realInterval)
            {
                lastRealInterval = Time.time;
                samples.Add(transform.position);
            }
        }

        private void StopSimulation()
        {
            speedFactor.SetValue(1);

            // TODO call generate level again. Send generated samples
        }
    }
}
