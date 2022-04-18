using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    private float speed = 20f;
    private Vector3 direction = new Vector3(1f,0f,-1f);
    private Rigidbody rb;
    private float lifeTime = 3.0f;


    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.FindWithTag("GameController").GetComponent<GameManager>().SetGameOver(true);
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
