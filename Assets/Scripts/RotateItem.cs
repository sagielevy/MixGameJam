using UnityEngine;

namespace Assets.Scripts
{
    public class RotateItem : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1.5f;
        [SerializeField] private Vector3 rotationVector = Vector3.zero;
        //[SerializeField] private float moveSpeed = 0.5f;
        //[SerializeField] private Vector3 directionVector = Vector3.zero;

        public void SetRotateData(float rotateSpeed, Vector3 rotationVector)
        {
            this.rotateSpeed = rotateSpeed;
            this.rotationVector = rotationVector.normalized;
        }

        void Start()
        {
            //directionVector = directionVector.normalized;
            rotationVector = rotationVector.normalized;
        }

        void Update()
        {
            //transform.position += directionVector * Time.deltaTime * moveSpeed;
            transform.rotation *= Quaternion.Euler(rotationVector * Time.deltaTime * rotateSpeed);
        }
    }
}
