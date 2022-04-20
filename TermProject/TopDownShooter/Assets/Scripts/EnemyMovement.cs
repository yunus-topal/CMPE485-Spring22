using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Camera mainCamera;
    private float speed = 10f;
    private GameObject player;
    public GameObject enemyBullet;
    public GameObject lockedInBullet;
    
    public void SetCamera(Camera cam)
    {
        this.mainCamera = cam;
        player = GameObject.FindWithTag("Player");
        if (gameObject.transform.CompareTag("DefaultEnemy"))
        {
            InvokeRepeating(nameof(DefaultAttackPlayer),1.0f,1.0f);
        } else if(gameObject.transform.CompareTag("LockedinEnemy"))
        {
            InvokeRepeating(nameof(LockedinAttackPlayer),2.0f,2.0f);

        }
    }

    private void DefaultAttackPlayer()
    {
        GameObject bullet = Instantiate(enemyBullet,new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity);
        bullet.GetComponent<EnemyBulletMovement>().SetAngle(transform.eulerAngles.y); 
    }

    private void LockedinAttackPlayer()
    {
        GameObject bullet = Instantiate(lockedInBullet,new Vector3(transform.position.x, 5f, transform.position.z), Quaternion.identity);
        bullet.GetComponent<LockedinBulletMovement>().Initialize(transform.eulerAngles.y, player); 
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("GameController").GetComponent<GameManager>().GetGameOver())
        {
            CancelInvoke();
        }
        else
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
    
}
