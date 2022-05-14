using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float hp = 1f;
    private GameObject gameManager;
    private Camera mainCamera;
    private float speed = 10f;
    private GameObject player;
    public GameObject enemyBullet;
    public GameObject lockedInBullet;
    private Animator enemyAnimator;
    private bool isDying = false;

    public void Initialize(Camera cam)
    {
        mainCamera = cam;
        gameManager = GameObject.FindWithTag("GameController");
        enemyAnimator = gameObject.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        if (gameObject.transform.CompareTag("DefaultEnemy"))
        {
            //InvokeRepeating(nameof(DefaultAttackPlayer),1.0f,1.0f);
            StartCoroutine(DefaultAttackPlayer());
        } else if(gameObject.transform.CompareTag("LockedinEnemy"))
        {
            //InvokeRepeating(nameof(LockedinAttackPlayer),2.0f,2.0f);
            StartCoroutine(LockedinAttackPlayer());
        }
    }

    private IEnumerator DefaultAttackPlayer()
    {
        while (!isDying)
        {
            enemyAnimator.SetTrigger("attack_trig");
            yield return new WaitForSeconds(0.25f);
            if(isDying) break;
            GameObject bullet = Instantiate(enemyBullet,new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity);
            bullet.GetComponent<EnemyBulletMovement>().Initialize(transform.eulerAngles.y); 
            yield return new WaitForSeconds(0.75f);
        }
    }

    private IEnumerator LockedinAttackPlayer()
    {
        while (!isDying)
        {
            enemyAnimator.SetTrigger("attack_trig");
            yield return new WaitForSeconds(0.75f);
            if(isDying) break;
            GameObject bullet = Instantiate(lockedInBullet,new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity);
            bullet.GetComponent<LockedinBulletMovement>().Initialize(transform.eulerAngles.y, player);
            yield return new WaitForSeconds(1.25f);
        }

    }

    private IEnumerator DestroySelf()
    {
        isDying = true;
        enemyAnimator.SetTrigger("die_trig");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gameManager = GameObject.FindWithTag("GameController");
        if (gameManager.GetComponent<GameManager>().GetGameOver())
        {
            StopAllCoroutines();
            return;
        }
        if(!isDying)
        {
            // get in the scene and attack player.
            if (transform.position.z > mainCamera.transform.position.z + 15f)
            {
                transform.position += new Vector3(0,0,-1f * speed * Time.deltaTime);
            }
        
            Vector3 lookDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0f, -angle, 0f);
        }

        
    }
    public void TakeDamage(float f)
    {
        hp -= f;
        if (hp <= 0)
        {
            StartCoroutine(DestroySelf());
            gameManager.GetComponent<GameManager>().IncreaseScore(tag);
        }
    }
    
}
