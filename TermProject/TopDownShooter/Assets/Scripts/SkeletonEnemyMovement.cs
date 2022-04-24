using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyMovement : MonoBehaviour
{
    private GameObject player;
    private Vector3 direction = new Vector3(1f,0f,-1f);
    private Rigidbody rb;
    private GameObject gameManager;
    private Animator skeletonAnimator;
    private bool isAttacking = false;
    private float speed = 10f;

    private bool isDying = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
        gameManager = GameObject.FindWithTag("GameController");
        skeletonAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().GetGameOver() && !isAttacking && !isDying)
        {
            Vector3 lookDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;
            SetVelocity(-angle);
            transform.rotation = Quaternion.Euler(0f, -angle, 0f);
            CheckDistance();
        }
        
    }
    
    private void SetVelocity(float angle)
    {
        direction.z = Mathf.Cos(angle * Mathf.Deg2Rad);
        direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
        rb.velocity = direction * speed;
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (distance < 3)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        rb.velocity = new Vector3(0, 0, 0);
        skeletonAnimator.SetTrigger("attack_trig");
        yield return new WaitForSeconds(1.1f);
        isAttacking = false;
    }
    public IEnumerator DestroySelf()
    {
        isDying = true;
        skeletonAnimator.SetTrigger("die_trig");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}