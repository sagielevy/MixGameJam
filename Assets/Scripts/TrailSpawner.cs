﻿using System.Collections.Generic;
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
        private List<Vector3> samples;
        private float startTime;
        private float lastSampleTime;

        private void Start()
        {
            samples = new List<Vector3>();
            trailRenderer = GetComponent<TrailRenderer>();
            trailRenderer.time = animateTimeSeconds.GetValue(); // TODO set more?
            startTime = Time.time;
        }

        public List<Vector3> GetSampledLocations()
        {
            return samples;
        }

        private void FixedUpdate()
        {
            if (Time.time - startTime >= animateTimeSeconds.GetValue())
            {
                return;
            }

            if (Time.time - lastSampleTime >= sampleIntervalTimeSeconds.GetValue())
            {
                lastSampleTime = Time.time;
                samples.Add(transform.position);
            }
        }
    }
}
