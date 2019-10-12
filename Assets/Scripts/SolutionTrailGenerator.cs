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

        private List<Vector3> samples;
        private Action<ITrail> simulationComplete;

        private void Awake()
        {
            samples = new List<Vector3>();
        }

        public void StartSimulation(Action<ITrail> simulationComplete)
        {
            samples.Clear();
            solutionRenderer.SetPositions(new Vector3[]{});
            this.simulationComplete = simulationComplete;
            animate.SetValue(true);
            Time.timeScale = simulationSpeed;
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (samples.Count >= samplesCount.GetValue())
            {
                StopSimulation();
                return;
            }

            samples.Add(transform.position);
        }

        private void StopSimulation()
        {
            if (simulationComplete == null) return;

            animate.SetValue(false);
            Time.timeScale = 1;
            simulationComplete(this);
            simulationComplete = null;

            Debug.Log($"Real sampled {samples.Count}, samples:");
            foreach (var sampledLocation in samples)
            {
                Debug.Log(sampledLocation);
            }

            solutionRenderer.SetPositions(samples.ToArray());
        }
    }
}
