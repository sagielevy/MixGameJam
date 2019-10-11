using UnityEngine;

namespace Assets.Scripts
{
    public class ItemAnimator : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1.5f;
        [SerializeField] private Vector3 rotationVector = Vector3.zero;
        [SerializeField] private BooleanReference animate;

        public int Id { get; private set; }

        public void SetItemData(int id, float rotateSpeed, Vector3 rotationVector)
        {
            Id = id;
            this.rotateSpeed = rotateSpeed;
            this.rotationVector = rotationVector.normalized;
        }

        void Start()
        {
            rotationVector = rotationVector.normalized;
        }

        void Update()
        {
            if (animate.GetValue())
            {
                transform.rotation *=
                    Quaternion.Euler(rotationVector * Time.deltaTime * rotateSpeed);
            }
        }
    }
}
