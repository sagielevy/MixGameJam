using UnityEngine;
using Assets.Scripts.TrailValidators.MockData;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Sandbox
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private int numberOfLines;
        void Start()
        {
            Vector3 prev = new Vector3(0,0,0);

            foreach (int index in Enumerable.Range(0, numberOfLines))
            {
                var dataFactory = new MockDataFactory();
                var current = dataFactory.YieldNewPoint();
                Debug.DrawLine(prev, current, Color.blue, 200);
                prev = current;
            }
        }

        void Update()
        {

        }
    }
}
