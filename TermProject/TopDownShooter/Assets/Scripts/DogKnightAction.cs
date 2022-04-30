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
    public LayerMask enemyLayers;
    private Animator dogAnimator;
    private DogKnightMovement movementScript;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        dogAnimator = gameObject.GetComponent<Animator>();
        movementScript = gameObject.GetComponent<DogKnightMovement>();
    }

    // Update is called once per frame
    void Update()
    {
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
                dogAnimator.SetTrigger("defend_trig");
                coolDown = 0.3f;
            }
        }
        if (coolDown > 0f)
        { 
            coolDown -= Time.deltaTime;
        }
    }

    public float GetCoolDown()
    {
        return coolDown;
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
                hitEnemy.gameObject.GetComponent<SkeletonEnemyMovement>().StartCoroutine(hitEnemy.gameObject.GetComponent<SkeletonEnemyMovement>().DestroySelf());
                gameManager.GetComponent<GameManager>().IncreaseScore(hitEnemy.gameObject.tag);
            }
            else if (hitEnemy.gameObject.tag.Contains("Enemy"))
            {
                hitEnemy.gameObject.GetComponent<EnemyMovement>().StartCoroutine(hitEnemy.gameObject.GetComponent<EnemyMovement>().DestroySelf());
                gameManager.GetComponent<GameManager>().IncreaseScore(hitEnemy.gameObject.tag);
            }
        }
    }
}
