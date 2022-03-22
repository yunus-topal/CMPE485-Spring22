using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject manager;
    private GameManager gm;
    private float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        gm = manager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.getGameOver())
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(1f * speed * Time.deltaTime, 0, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position += new Vector3(-1f * speed * Time.deltaTime, 0, 0);
            }

            if (transform.position.x < -40 && gm.getGoldPicked())
            {
                gm.setGameOver();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // pick up gold
        if (other.gameObject.tag == "Treasure")
        {
            gm.setGoldPicked(true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Barrier")
        {
            gm.setPlayerDead(true);
            gm.setGameOver();
            Destroy(gameObject);

        }
    }

}
