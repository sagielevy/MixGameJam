using UnityEngine;

public class RotateItem : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1.5f;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private Vector3 rotationVector = Vector3.zero;
    [SerializeField] private Vector3 directionVector = Vector3.zero;

    void Start()
    {
        directionVector = directionVector.normalized;
        rotationVector = rotationVector.normalized;
    }

    void Update()
    {
        transform.position += directionVector * Time.deltaTime * moveSpeed;
        transform.rotation *= Quaternion.Euler(rotationVector * Time.deltaTime * rotateSpeed);
    }
}
