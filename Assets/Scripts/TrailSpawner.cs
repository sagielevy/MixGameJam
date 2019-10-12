using System.Collections.Generic;
using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{ 
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailSpawner : MonoBehaviour, ITrail
    {
        [SerializeField] private BooleanReference animate;
        [SerializeField] private IntReference samplesCount;

        private TrailRenderer trailRenderer;
        private List<Vector3> samples;

        private void Start()
        {
            samples = new List<Vector3>();
            trailRenderer = GetComponent<TrailRenderer>();
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (samples.Count >= samplesCount.GetValue())
            {
                animate.SetValue(false);
                return;
            }

            samples.Add(transform.position);
        }
    }
}
