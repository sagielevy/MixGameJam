using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.TrailValidators.MockData
{
    public class MockDataFactory
    {
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
        private int t = 0;
        private IEnumerator<Vector3> dataPointsGenerator;

        public MockDataFactory()
        {
            t = 0;
            dataPointsGenerator = DataPointsGenerator();
        }

        public IEnumerator<Vector3> DataPointsGenerator()
        {
            while (true) { 
                // z(t) = f(x(t),y(t)) = f(at,bt) = c + t[ad+be]+t^2[fab+ga^2+hb^2]
                var x = x0 + a * t;
                var y = y0 + b * t;
                var z = c + d * x + e * y + f * x * y + g * x * x + h * y * y;
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
