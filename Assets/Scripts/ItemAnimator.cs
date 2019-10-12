using Assets.Scripts.SharedData;
using UnityEngine;

namespace Assets.Scripts
{
    public class ItemAnimator : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed = 1.5f;
        [SerializeField] private Vector3 rotationVector = Vector3.zero;
        [SerializeField] private BooleanReference animate;
        [SerializeField] private BooleanReference levelClickable;
        [SerializeField] private int Id;

        private ArrowHintRotator arrowsHint;

        public void SetItemData(int id, float rotateSpeed, Vector3 rotationVector, ArrowHintRotator arrowsHint)
        {
            Id = id;
            this.rotateSpeed = rotateSpeed;
            this.rotationVector = rotationVector.normalized;
            this.arrowsHint = arrowsHint;
            transform.rotation = Quaternion.identity;
        }

        public int GetId()
        {
            return Id;
        }

        void FixedUpdate()
        {
            if (arrowsHint != null)
            {
                arrowsHint.gameObject.SetActive(!animate.GetValue() && levelClickable.GetValue());
            }

            if (animate.GetValue())
            {
                transform.rotation *=
                    Quaternion.Euler(rotationVector * Time.fixedDeltaTime * rotateSpeed);
            }
        }
    }
}
