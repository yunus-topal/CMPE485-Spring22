using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");
        
        transform.position += new Vector3(horizontalSpeed * Time.deltaTime * speed,0,verticalSpeed * Time.deltaTime * speed);
        // stay in z axis
        if (transform.position.z < mainCamera.gameObject.transform.position.z - 18)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
                mainCamera.gameObject.transform.position.z - 18);
        }
        
        // stay in x axis
        if (transform.position.x > 25)
        {
            transform.position = new Vector3(25f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -25)
        {
            transform.position = new Vector3(-25f, transform.position.y, transform.position.z);
        }
        // move camera if player moves too far
        if (transform.position.z > mainCamera.gameObject.transform.position.z)
        {
            mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x,
                mainCamera.gameObject.transform.position.y, transform.position.z);
        }
    }
}
