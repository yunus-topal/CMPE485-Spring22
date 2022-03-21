using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMovement : MonoBehaviour
{

    public float speed = 100f;
    private Rigidbody rb;
    private float direction = 1f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > 10)
        {
            direction = -1f;
            rb.velocity = new Vector3(0, 0, 0);
        }
        if (transform.position.z < -10)
        {
            direction = 1f;
            rb.velocity = new Vector3(0, 0, 0);
        }


        rb.AddForce(Vector3.forward * direction * Time.deltaTime * speed, ForceMode.Impulse);

    }

}
