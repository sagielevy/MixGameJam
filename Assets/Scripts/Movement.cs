using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speed;
    public Transform target;

    void Update()
    {
        transform.LookAt(target);

        if (Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(transform.up * Input.GetAxis("Vertical") * Time.deltaTime * speed); //.up = positive y
        }

        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed); //.right = positive x
        }
    }
}