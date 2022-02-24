using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Rigidbody rb;
    private float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GameObject.FindObjectOfType<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.left * Time.deltaTime * speed, ForceMode.Impulse);
    }
}
