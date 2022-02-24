using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject sphere;
    private float speed = 15f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = sphere.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.AddForce(Vector3.right * verticalInput * Time.deltaTime * speed, ForceMode.Impulse);
        rb.AddForce(Vector3.back * horizontalInput * Time.deltaTime * speed, ForceMode.Impulse);

        mainCamera.transform.position = new Vector3(rb.transform.position.x - 3, rb.transform.position.y + 10, rb.transform.position.z);


        // if (Input.GetKey(KeyCode.W))
        // {
        //     rb.AddForce(Vector3.right * Time.deltaTime * speed, ForceMode.Impulse);
        // }
        // if (Input.GetKey(KeyCode.S))
        // {
        //     rb.AddForce(Vector3.left * Time.deltaTime * speed, ForceMode.Impulse);
        // }

        // if (Input.GetKey(KeyCode.A))
        // {
        //     rb.AddForce(Vector3.forward * Time.deltaTime * speed, ForceMode.Impulse);
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     rb.AddForce(Vector3.back * Time.deltaTime * speed, ForceMode.Impulse);
        // }

    }
}
