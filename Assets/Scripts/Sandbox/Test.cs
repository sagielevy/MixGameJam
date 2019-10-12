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

        [SerializeField] private float x0;
        [SerializeField] private float y0;
        [SerializeField] private float at;
        [SerializeField] private float bt;
        [SerializeField] private float z0;
        [SerializeField] private float dx;
        [SerializeField] private float ey;
        [SerializeField] private float fxy;
        [SerializeField] private float gxx;
        [SerializeField] private float hyy;

        [SerializeField] private float acosx;
        [SerializeField] private float bsiny;
        [SerializeField] private float hz;
        [SerializeField] private float tphaseReference;
        [SerializeField] private float tphaseMoving;

        [SerializeField] private float samplingRate;
        [SerializeField] private float tSamplesDiff;

        ITrailValidator validator;

        void Start()
        {
            validator = new TrailValidatorMSE(0.1f);
        }

        void Update()
        {
            void DrawLine(Vector3 start, Vector3 end, Color color)
            {
                Debug.LogFormat($"start: {start}, end: {end}");
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
            
            var referenceDataFactory = new MockDataFactory(this, tphaseReference, 0);
            var movingDataFactory = new MockDataFactory(this, tphaseMoving, tSamplesDiff);

            var referenceSamples = SampleTrailFromPointFactory(referenceDataFactory);
            var movingSamples = SampleTrailFromPointFactory(movingDataFactory);

            DrawSampledTrails(referenceSamples, Color.blue);
            DrawSampledTrails(movingSamples, Color.red);
        }

        private class MockDataFactory
        {

            private int t = 0;
            private IEnumerator<Vector3> dataPointsGenerator;
            private Test test;

            public MockDataFactory(Test te, float phase, float tSamplesDiff)
            {
                test = te;
                t = 0;
                //dataPointsGenerator = parabolaPointsGenerator();
                dataPointsGenerator = SpiralPointsGenerator(phase, tSamplesDiff);
            }

            public IEnumerator<Vector3> parabolaPointsGenerator()
            {
                while (true)
                {
                    // x,y = a,b * t
                    // z(t) = c + dx + ey + fxy + gxx + hyy
                    var x = test.x0 + test.at * t;
                    var y = test.y0 + test.bt * t;
                    var z = test.z0 + test.dx * x + test.ey * y + test.fxy * x * y + test.gxx * x * x + test.hyy * y * y;
                    yield return new Vector3(x, y, z);
                    t++;
                }
            }

            public IEnumerator<Vector3> SpiralPointsGenerator(float phase, float tSamplesDiff)
            {
                while (true)
                {
                    // x = a cos(bt)+x0
                    // y = b sin(dt)+y0
                    // z = gt+z0
                    float effectiveT = t + tSamplesDiff;
                    var x = test.x0 + test.acosx * Mathf.Cos(test.samplingRate * Mathf.PI * 2 * effectiveT + phase);
                    var y = test.y0 + test.bsiny * Mathf.Sin(test.samplingRate * Mathf.PI * 2 * effectiveT + phase);
                    var z = test.z0 + test.hz * effectiveT;
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
