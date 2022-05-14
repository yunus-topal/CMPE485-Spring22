using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogKnightMovement : MonoBehaviour
{
    private float hp = 20f;
    public Camera mainCamera;
    private float speed = 15f;
    private Animator playerAnimator;

    private bool dashing = false;

    private float dashingTime = 0.15f;

    private float dashingCd = 0f;

    private GameObject gameManager;
    private DogKnightAction dogAction;
    public Slider hpBar;

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
        if (!gameManager.GetComponent<GameManager>().GetGameOver())
        {
            bool bossPhase = gameManager.GetComponent<GameManager>().GetBossPhase();
            CheckMovement();
            StayInLine(bossPhase);
            SetRotation();
        }
    }

    public bool GetDashing()
    {
        return dashing;
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
        if (transform.position.z > 25)
        {
            float diff = transform.position.z - 25f;
            transform.position -= new Vector3(0, 0,diff);
        }
        else if (transform.position.z < -25)
        {
            float diff = transform.position.z + 25f;
            transform.position -= new Vector3(0, 0,diff);
        }
        
        // stay in x axis
        if (transform.position.x > 50)
        {
            float diff = transform.position.x - 50f;
            transform.position -= new Vector3(diff, 0,0);
        }
        else if (transform.position.x < -50)
        {
            float diff = transform.position.x + 50f;
            transform.position -= new Vector3(diff, 0,0);
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

    public void GetHit(float f)
    {
        if (!gameObject.GetComponent<DogKnightAction>().getRage())
        {
            hp -= f;
            if (hp <= 0)
            {
                hpBar.value = 0;
                StartCoroutine(DestroySelf());
            }
            else
            {
                hpBar.value = hp / 20f;
            }
        }
       
    }

    private IEnumerator DestroySelf()
    {
        gameManager.GetComponent<GameManager>().SetGameOver(true);
        playerAnimator.SetTrigger("die_trig");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
