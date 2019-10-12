using UnityEngine;

using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

namespace Assets.Scripts.Sandbox
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private int numberOfLines;

        [SerializeField] private float x0Moving;
        [SerializeField] private float y0Moving;
        [SerializeField] private float z0Moving;
        [SerializeField] private float acosxMoving;
        [SerializeField] private float bsinyMoving;
        [SerializeField] private float hzMoving;
        [SerializeField] private float tphaseMoving;
        [SerializeField] private float samplingRateMoving;
        [SerializeField] private float samplingDiffMoving;

        [SerializeField] private float x0Reference;
        [SerializeField] private float y0Reference;
        [SerializeField] private float z0Reference;
        [SerializeField] private float acosxReference;
        [SerializeField] private float bsinyReference;
        [SerializeField] private float hzReference;
        [SerializeField] private float tphaseReference;
        [SerializeField] private float samplingRateReference;
        [SerializeField] private float samplingDiffReference;

        [SerializeField] private float allowedError;

        ITrailValidator validator;

        void Start()
        {
            
        }

        void Update()
        {
            validator = new TrailValidatorMSE();
            void DrawLine(Vector3 start, Vector3 end, Color color)
            {
                //Debug.LogFormat($"start: {start}, end: {end}");
                Debug.DrawLine(start, end, color, 0.1f);
            }

            ITrail SampleTrailFromPointFactory(MockDataFactory factory)
            {
                var samples = new List<Vector3>();
                foreach (int index in Enumerable.Range(0, numberOfLines))
                {
                    samples.Add(factory.YieldNewPoint());
                }
                return new SampledTrail(samples);
            }

            void DrawSampledTrails(ITrail trail, Color color)
            {
                Vector3 prev = new Vector3(0, 0, 0);
                foreach (Vector3 v in trail.GetSampledLocations())
                {
                    var current = v;
                    DrawLine(prev, current, color);
                    prev = current;
                }
            }
            
            var referenceDataFactory = new MockDataFactory(this, true);
            var movingDataFactory = new MockDataFactory(this, false);

            var referenceSamples = SampleTrailFromPointFactory(referenceDataFactory);
            var movingSamples = SampleTrailFromPointFactory(movingDataFactory);

            DrawSampledTrails(referenceSamples, Color.blue);
            DrawSampledTrails(movingSamples, Color.red);

            ValidateCurves(referenceSamples, movingSamples);
        }

        private void ValidateCurves(ITrail referenceSamples, ITrail movingSamples)
        {
            if (validator.Validate(referenceSamples, movingSamples, allowedError))
            {
                Debug.LogFormat("Curves are close enough");
            }
            else
            {
                Debug.LogFormat("Curves are too far");
            }
        }

        private class MockDataFactory
        {

            private int t = 0;
            private IEnumerator<Vector3> dataPointsGenerator;
            private Test test;

            public MockDataFactory(Test te, bool isMoving)
            {
                test = te;
                t = 0;
                //dataPointsGenerator = parabolaPointsGenerator();
                if (isMoving){
                    dataPointsGenerator = SpiralPointsGenerator(te.tphaseMoving, te.samplingDiffMoving, te.samplingRateMoving, te.x0Moving, te.y0Moving, te.z0Moving, te.acosxMoving, te.bsinyMoving, te.hzMoving);
                }
                else {
                    dataPointsGenerator = SpiralPointsGenerator(te.tphaseReference, te.samplingDiffReference, te.samplingRateReference, te.x0Reference, te.y0Reference, te.z0Reference, te.acosxReference, te.bsinyReference, te.hzReference);
                }
                
            }
            
            public IEnumerator<Vector3> SpiralPointsGenerator(float phase, float tSamplesDiff, float samplingRate, float x0, float y0, float z0, float acosx, float bsiny, float hz)
            {
                while (true)
                {
                    // x = a cos(bt)+x0
                    // y = b sin(dt)+y0
                    // z = gt+z0
                    float effectiveT = t + tSamplesDiff;
                    var x = x0 + acosx * Mathf.Cos(samplingRate * Mathf.PI * 2 * effectiveT + phase);
                    var y = y0 + bsiny * Mathf.Sin(samplingRate * Mathf.PI * 2 * effectiveT + phase);
                    var z = z0 + hz * effectiveT;
                    yield return new Vector3(x, y, z);
                    t++;
                }
            }

            public Vector3 YieldNewPoint()
            {
                dataPointsGenerator.MoveNext();
                return dataPointsGenerator.Current;
            }
        }
    }
}
