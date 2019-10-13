using UnityEngine;

namespace Assets.Scripts
{
    public class ArrowHintRotator : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1.5f;
        [SerializeField] private Transform localRotate;

        private void Update()
        {
            localRotate.localRotation =
                Quaternion.Euler(Vector3.down * Time.deltaTime * rotateSpeed) * localRotate.localRotation;
        }
    }
}
