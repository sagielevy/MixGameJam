using System;
using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{
    public class SolutionTrailGenerator : MonoBehaviour, ITrail
    {
        [SerializeField] private BooleanReference animate;
        [SerializeField] private FloatReference sampleIntervalSeconds;
        [SerializeField] private FloatReference animateTimeSeconds;
        [SerializeField] private FloatReference speedFactor;
        [SerializeField] private float simulationSpeed;

        private List<Vector3> samples;
        private float startTime;
        private float lastRealInterval;
        private float realInterval;
        private float realAnimateTime;
        private Action<ITrail> simulationComplete;

        private void Awake()
        {
            samples = new List<Vector3>();
        }

        public void StartSimulation(Action<ITrail> simulationComplete)
        {
            samples.Clear();
            this.simulationComplete = simulationComplete;
            speedFactor.SetValue(simulationSpeed);
            animate.SetValue(true);
            realInterval = sampleIntervalSeconds.GetValue() / speedFactor.GetValue();
            realAnimateTime = animateTimeSeconds.GetValue() / speedFactor.GetValue();
            startTime = Time.unscaledTime;
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (Time.unscaledTime - startTime >= realAnimateTime)
            {
                StopSimulation();
                return;
            }

            if (Time.unscaledTime - lastRealInterval >= realInterval)
            {
                lastRealInterval = Time.unscaledTime;
                samples.Add(transform.position);
            }
        }

        private void StopSimulation()
        {
            if (simulationComplete == null) return;

            animate.SetValue(false);
            speedFactor.SetValue(1);
            simulationComplete(this);
            simulationComplete = null;
        }
    }
}
