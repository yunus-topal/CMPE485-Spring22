using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnightAction : MonoBehaviour
{
    private GameObject gameManager;
    public float coolDown = 0f;
    public GameObject attackPoint;
    private float attackRange = 5f;
    private float parryRange = 3f;
    public LayerMask enemyLayers;
    private Animator dogAnimator;
    private DogKnightMovement movementScript;
    private int parryCount = 0;
    private float rageDuration = 0f;
    private bool isOnRage = false;
    
    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        dogAnimator = gameObject.GetComponent<Animator>();
        movementScript = gameObject.GetComponent<DogKnightMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().GetGameOver())
        {
            // actions
            if (Input.GetKey(KeyCode.Mouse0) && !movementScript.GetDashing())
            {
                if (coolDown <= 0f)
                {
                    StartCoroutine(Attack());
                }
            }
            else if (Input.GetKey(KeyCode.Mouse1) && !movementScript.GetDashing())
            {
                if (coolDown <= 0f)
                {
                    StartCoroutine(Parry());
                }
            }
            else if (Input.GetKey(KeyCode.E) && !movementScript.GetDashing() && parryCount > 10 && !isOnRage)
            {
                if (coolDown <= 0f)
                {
                    StartCoroutine(Rage());
                }
            }
        }
        
        if (coolDown > 0f)
        { 
            coolDown -= Time.deltaTime;
        }
        if (rageDuration > 0f)
        { 
            rageDuration -= Time.deltaTime;
            if (rageDuration <= 0f)
            {
                StartCoroutine(Shrink());
            }
        }
    }

    public float GetCoolDown()
    {
        return coolDown;
    }

    IEnumerator Shrink()
    {
        float time = 1f;
        for (float f = 0; f < time; f += Time.deltaTime)
        {
            float decrease = (2f / time) * Time.deltaTime;
            transform.localScale -= new Vector3(decrease,decrease,decrease);
            attackRange -= decrease / 2;
            parryRange -= decrease / 2;

            yield return null;
        }
        rageDuration = 0f;
        isOnRage = false;
    }
    IEnumerator Rage()
    {
        isOnRage = true;
        parryCount = 0;
        dogAnimator.SetTrigger("rage_trig");
        coolDown = 1f;
        float time = 1f;
        for (float f = 0; f < time; f += Time.deltaTime)
        {
            float increase = (2f / time) * Time.deltaTime;
            transform.localScale += new Vector3(increase,increase,increase);
            attackRange += increase / 2;
            parryRange += increase / 2;
            yield return null;
        }

        rageDuration = 5f;
    }
    IEnumerator Parry()
    {
        dogAnimator.SetTrigger("defend_trig");
        coolDown = 0.3f;
        yield return new WaitForSeconds(0.2f);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, parryRange, enemyLayers);
        foreach (Collider hitEnemy in hitEnemies)    
        {
            if (hitEnemy.gameObject.tag.Contains("Bullet"))
            {
                Destroy(hitEnemy.gameObject);
                parryCount += 1;
            }
            
        }
    }
    IEnumerator Attack()
    {
        dogAnimator.SetTrigger("attack_trig");
        coolDown = 0.3f;
        yield return new WaitForSeconds(0.2f);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, attackRange, enemyLayers);
        foreach (Collider hitEnemy in hitEnemies)    
        {
            if (hitEnemy.gameObject.CompareTag("SkeletonEnemy") || hitEnemy.gameObject.CompareTag("KnightEnemy"))
            {
                hitEnemy.gameObject.GetComponent<SkeletonEnemyMovement>().TakeDamage();
            }
            else if (hitEnemy.gameObject.tag.Contains("Enemy"))
            {
                hitEnemy.gameObject.GetComponent<EnemyMovement>().TakeDamage();
            }
            else if (hitEnemy.gameObject.tag.Contains("Boss"))
            {
                hitEnemy.gameObject.GetComponent<BossMovement>().TakeDamage();
            }
        }
    }
}
