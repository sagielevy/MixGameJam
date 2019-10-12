using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{ 
    [RequireComponent(typeof(LineRenderer))]
    public class TrailSpawner : MonoBehaviour, ITrail
    {
        [SerializeField] private BooleanReference animate;
        [SerializeField] private IntReference samplesCount;

        private LineRenderer lineRenderer;
        private Vector3[] samples;
        private int currSampleIndex;

        private void Start()
        {
            samples = new Vector3[samplesCount.GetValue()];
            lineRenderer = GetComponent<LineRenderer>();
            currSampleIndex = 0;
        }

        public IEnumerable<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (currSampleIndex == samples.Length)
            {
                animate.SetValue(false);
                return;
            }

            lineRenderer.positionCount = currSampleIndex;
            samples[currSampleIndex] = transform.position;
            lineRenderer.SetPositions(samples);
            currSampleIndex++;
        }
    }
}
