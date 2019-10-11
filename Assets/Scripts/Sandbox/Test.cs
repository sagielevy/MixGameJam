using UnityEngine;

using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Scripts.Sandbox
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private int numberOfLines;

        [SerializeField] private float x0;
        [SerializeField] private float y0;
        [SerializeField] private float at;
        [SerializeField] private float bt;
        [SerializeField] private float c;
        [SerializeField] private float dx;
        [SerializeField] private float ey;
        [SerializeField] private float fxy;
        [SerializeField] private float gxx;
        [SerializeField] private float hyy;

        void Start()
        {
            Vector3 prev = new Vector3(0,0,0);
            var dataFactory = new MockDataFactory(this);
            foreach (int index in Enumerable.Range(0, numberOfLines))
            {
                var current = dataFactory.YieldNewPoint();
                Debug.LogFormat($"current: {current}, prev: {prev}");
                Debug.DrawLine(prev, current, Color.blue, 200);
                prev = current;
            }
        }

        void Update()
        {

        }


        private class MockDataFactory
        {

            private int t = 0;
            private IEnumerator<Vector3> dataPointsGenerator;
            private Test test;

            public MockDataFactory(Test te)
            {
                test = te;
                t = 0;
                dataPointsGenerator = DataPointsGenerator();
            }

            public IEnumerator<Vector3> DataPointsGenerator()
            {
                while (true)
                {
                    // x,y = a,b * t
                    // z(t) = c + dx + ey + fxy + gxx + hyy
                    var x = test.x0 + test.at * t;
                    var y = test.y0 + test.bt * t;
                    var z = test.c + test.dx * x + test.ey * y + test.fxy * x * y + test.gxx * x * x + test.hyy * y * y;
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
