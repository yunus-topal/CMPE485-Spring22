using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 10f;
    private Animator playerAnimator;
    private Rigidbody rb;
    private float dashCd = 0f;
    private float dashingTime = 0f;

    private bool dashing = false;
// Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");
        // switch between running and idle animation
        if ((horizontalSpeed != 0 || verticalSpeed != 0) && dashingTime <= 0)
        {
            playerAnimator.SetFloat("speed_f",0.9f);
            if (Input.GetKeyDown(KeyCode.Space) && dashCd <= 0)
            {
                Move(horizontalSpeed, verticalSpeed, true);
                dashCd = 1.0f;
                dashingTime = 0.2f;
                dashing = true;
            }
            else
            {
                Move(horizontalSpeed, verticalSpeed, false);
            }
        }
        else
        {
            playerAnimator.SetFloat("speed_f",0f);
        }
        
  
        Vector3 lookDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, -angle, 0f);
        if (dashCd > 0)
        {
            dashCd -= Time.deltaTime;
        }

        if (dashingTime > 0)
        {
            dashingTime -= Time.deltaTime;
            if (dashing && dashingTime <= 0)
            {
                dashing = false;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }
    void Move(float horizontalSpeed, float verticalSpeed, bool dash)
    {
        if (dash)
        {
            rb.velocity = new Vector3(horizontalSpeed * speed * 4f,0,verticalSpeed * speed * 4f);
        }
        else if(rb.velocity.magnitude < new Vector3(horizontalSpeed * speed,0,verticalSpeed * speed).magnitude)
        {
            rb.velocity = new Vector3(horizontalSpeed * speed,0,verticalSpeed * speed);
        }

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
    }
}
