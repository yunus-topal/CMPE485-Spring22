using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMovement : MonoBehaviour
{
    private float speed = 40f;
    private Vector3 direction = new Vector3(1f,0f,1f);
    private Rigidbody rb;
    private float lifeTime = 1.75f;


    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SkeletonEnemy"))
        {
            collision.gameObject.GetComponent<SkeletonEnemyMovement>().StartCoroutine(collision.gameObject.GetComponent<SkeletonEnemyMovement>().DestroySelf());
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag.Contains("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyMovement>().StartCoroutine(collision.gameObject.GetComponent<EnemyMovement>().DestroySelf());
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("LockedinBullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetAngle(float angle)
    {
        rb = GetComponent<Rigidbody>();
        direction.z = Mathf.Cos(angle * Mathf.Deg2Rad);
        direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
        rb.velocity = direction * speed;
    }
}
