using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float speed = 40f;
    private Vector3 direction = new Vector3(1f,0f,1f);
    private Rigidbody rb;
    private float lifeTime = 2f;


    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public void setAngle(float angle)
    {
        rb = GetComponent<Rigidbody>();
        direction.z = Mathf.Cos(angle * Mathf.Deg2Rad);
        direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
        rb.velocity = direction * speed;
    }
}
