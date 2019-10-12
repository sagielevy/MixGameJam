using System;
using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{
    public class SolutionTrailGenerator : MonoBehaviour, ITrail
    {
        [SerializeField] private BooleanReference animate;
        [SerializeField] private IntReference samplesCount;
        [SerializeField] private float simulationSpeed;
        [SerializeField] private LineRenderer solutionRenderer;

        private int currSampleIndex;
        private Vector3[] samples;
        private Action<ITrail> simulationComplete;

        private void Awake()
        {
            samples = new Vector3[samplesCount.GetValue()];
        }

        public void StartSimulation(Action<ITrail> simulationComplete)
        {
            currSampleIndex = 0;
            solutionRenderer.SetPositions(new Vector3[]{});
            this.simulationComplete = simulationComplete;
            animate.SetValue(true);
            Time.timeScale = simulationSpeed;
        }

        public IEnumerable<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (currSampleIndex == samples.Length)
            {
                StopSimulation();
                return;
            }

            samples[currSampleIndex] = transform.position;
            currSampleIndex++;
        }

        private void StopSimulation()
        {
            if (simulationComplete == null) return;

            animate.SetValue(false);
            Time.timeScale = 1;
            simulationComplete(this);
            simulationComplete = null;
            
            solutionRenderer.positionCount = samples.Length;
            solutionRenderer.SetPositions(samples);
        }
    }
}
