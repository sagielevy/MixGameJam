using UnityEngine;

namespace Assets.Scripts
{
    public class ArrowHintRotator : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1.5f;
        private Vector3 rotateDir;

        public void SetRotateDir(Vector3 rotateDir)
        {
            this.rotateDir = rotateDir;
        }

        private void Update()
        {
            transform.localRotation =
                Quaternion.Euler(rotateDir * Time.deltaTime * rotateSpeed) * transform.localRotation;
        }
    }
}
