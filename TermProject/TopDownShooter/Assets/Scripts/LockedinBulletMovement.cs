using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedinBulletMovement : MonoBehaviour
{
    private float speed = 10f;
    private Vector3 direction = new Vector3(1f,0f,-1f);
    private Rigidbody rb;
    private GameObject player;
    private GameObject gameManager;

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().GetGameOver())
        {
            // change direction of the bullet
            Vector3 lookDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg - 90f;
            SetVelocity(-angle);
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

    
    public void Initialize(float angle, GameObject playerCharacter)
    {
        gameManager = GameObject.FindWithTag("GameController");
        player = playerCharacter;
        rb = GetComponent<Rigidbody>();
        SetVelocity(angle);
    }

    private void SetVelocity(float angle)
    {
        direction.z = Mathf.Cos(angle * Mathf.Deg2Rad);
        direction.x = Mathf.Sin(angle * Mathf.Deg2Rad);
        rb.velocity = direction * speed;
    }
}
