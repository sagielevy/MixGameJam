using UnityEngine;

namespace Assets.Scripts.Sandbox
{
    public class Test : MonoBehaviour
    {
        void Start()
        {
            var start = new Vector3(1, 2, 3);
            var end = new Vector3(0, 0, 0);
            Debug.DrawLine(start, end, Color.blue, 200);
        }

        void Update()
        {

        }
    }
}
