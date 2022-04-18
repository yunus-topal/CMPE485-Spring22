using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    private float speed = 10f;
    private Animator playerAnimator;
// Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");
        // switch between running and idle animation
        if (horizontalSpeed != 0 || verticalSpeed != 0)
        {
            playerAnimator.SetFloat("speed_f",0.9f);
        }
        else
        {
            playerAnimator.SetFloat("speed_f",0f);
        }
        
        transform.position += new Vector3(horizontalSpeed * Time.deltaTime * speed,0,verticalSpeed * Time.deltaTime * speed);
        // stay in z axis
        if (transform.position.z < mainCamera.gameObject.transform.position.z - 15)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
                mainCamera.gameObject.transform.position.z - 15);
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

  
        Vector3 lookDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, -angle, 0f);
        
    }
}
