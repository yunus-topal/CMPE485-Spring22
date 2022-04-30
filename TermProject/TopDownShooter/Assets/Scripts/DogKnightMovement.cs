using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightMovement : MonoBehaviour
{

    public Camera mainCamera;
    private float speed = 15f;
    private Animator playerAnimator;

    private bool dashing = false;

    private float dashingTime = 0.15f;

    private float dashingCd = 0f;

    private GameObject gameManager;
    private DogKnightAction dogAction;

// Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        playerAnimator = GetComponent<Animator>();
        dogAction = gameObject.GetComponent<DogKnightAction>();
    }

    // Update is called once per frame
    void Update()
    {
        bool bossPhase = gameManager.GetComponent<GameManager>().GetBossPhase();
        if (!bossPhase)
        {
            CheckBossPhase();
        }else if (gameManager.GetComponent<GameManager>().GetBossTransition() >= 0)
        {
            playerAnimator.SetFloat("speed_f",0f);
            return;
        }
        CheckMovement();
        StayInLine(bossPhase);
        SetRotation();
    }

    public bool GetDashing()
    {
        return dashing;
    }

    bool CheckBossPhase()
    {
        float offset = mainCamera.transform.position.z - 125f;
        if (offset >= 0)
        {
            mainCamera.transform.position -= new Vector3(0,0,offset);
            gameManager.GetComponent<GameManager>().SetBossPhase();
            return true;
        }

        return false;
    }
    void CheckMovement()
    {
        float horizontalSpeed = Input.GetAxis("Horizontal");
        float verticalSpeed = Input.GetAxis("Vertical");
        // switch between running and idle animation
        if (!dashing)
        {
            if (horizontalSpeed != 0 || verticalSpeed != 0)
            {
                playerAnimator.SetFloat("speed_f",0.9f);
                if (Input.GetKeyDown(KeyCode.Space) && dashingCd <= 0 && dogAction.GetCoolDown() <= 0)
                {
                    dashingCd = 1.0f;
                    StartCoroutine(Dash(horizontalSpeed, verticalSpeed));
                }
                else
                {
                    transform.position += new Vector3(horizontalSpeed * Time.deltaTime * speed,0,verticalSpeed * Time.deltaTime * speed);
                }
            }
            else
            {
                playerAnimator.SetFloat("speed_f",0f);
            }
        }
    }
    void StayInLine(bool bossPhase)
    {
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
        if (transform.position.z > mainCamera.gameObject.transform.position.z && !bossPhase)
        {
            mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x,
                mainCamera.gameObject.transform.position.y, transform.position.z);
        }
    }
    void SetRotation()
    {
        Vector3 lookDir = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, -angle, 0f);
        if (dashingCd > 0)
        {
            dashingCd -= Time.deltaTime;
        }
    }

    IEnumerator Dash(float horizontalSpeed, float verticalSpeed)
    {
        dashing = true;
        for (float f = 0; f < dashingTime; f += Time.deltaTime)
        {
            transform.position += new Vector3(horizontalSpeed * Time.deltaTime * speed * 5f,0,verticalSpeed * Time.deltaTime * speed * 5f);
            yield return null;
        }

        dashing = false;
    }
}
