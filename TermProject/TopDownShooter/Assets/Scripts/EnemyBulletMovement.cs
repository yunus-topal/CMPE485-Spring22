using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletMovement : MonoBehaviour
{
    private float speed = 20f;
    private Vector3 direction = new Vector3(1f,0f,-1f);
    private Rigidbody rb;
    private float lifeTime = 3.0f;
    private GameObject gameManager;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0  || gameManager.GetComponent<GameManager>().GetBossPhase())
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.GetComponent<GameManager>().SetGameOver(true);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    public void Initialize(float angle)
    {
        gameManager = GameObject.FindWithTag("GameController");
        rb = GetComponent<Rigidbody>();
        SetAngle(angle);
    }
    public void SetAngle(float angle)
    {
        
        direction.z = Mathf.Cos(angle * Mathf.Deg2Rad);
        direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
        rb.velocity = direction * speed;
    }
}
