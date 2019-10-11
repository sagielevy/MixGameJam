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

        [SerializeField] private int x0;
        [SerializeField] private int y0;
        [SerializeField] private int a;
        [SerializeField] private int b;
        [SerializeField] private int c;
        [SerializeField] private int d;
        [SerializeField] private int e;
        [SerializeField] private int f;
        [SerializeField] private int g;
        [SerializeField] private int h;

        void Start()
        {
            Vector3 prev = new Vector3(0,0,0);

            foreach (int index in Enumerable.Range(0, numberOfLines))
            {
                var dataFactory = new MockDataFactory(this);
                var current = dataFactory.YieldNewPoint();
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
                    // z(t) = f(x(t),y(t)) = f(at,bt) = c + t[ad+be]+t^2[fab+ga^2+hb^2]
                    var x = test.x0 + test.a * t;
                    var y = test.y0 + test.b * t;
                    var z = test.c + test.d * x + test.e * y + test.f * x * y + test.g * x * x + test.h * y * y;
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
