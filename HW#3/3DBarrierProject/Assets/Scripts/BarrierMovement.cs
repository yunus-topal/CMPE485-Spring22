using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMovement : MonoBehaviour
{

    public float speed = 100f;
    private Rigidbody rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // rb.AddForce(Vector3.right * verticalInput * Time.deltaTime * speed, ForceMode.Impulse);
        rb.AddForce(Vector3.forward * horizontalInput * Time.deltaTime * speed, ForceMode.Impulse);

    }

}
