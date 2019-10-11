using UnityEngine;

namespace Assets.Scripts
{
    public class ItemAnimator : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1.5f;
        [SerializeField] private Vector3 rotationVector = Vector3.zero;

        public int Id { get; private set; }

        public void SetItemData(int id, float rotateSpeed, Vector3 rotationVector)
        {
            this.Id = id;
            this.rotateSpeed = rotateSpeed;
            this.rotationVector = rotationVector.normalized;
        }

        void Start()
        {
            rotationVector = rotationVector.normalized;
        }

        void Update()
        {
            transform.rotation *= Quaternion.Euler(rotationVector * Time.deltaTime * rotateSpeed);
        }
    }
}
