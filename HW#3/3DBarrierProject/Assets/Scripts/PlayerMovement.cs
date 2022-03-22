using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject manager;
    private GameManager gm;
    private GameObject treasure;
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

            if (Input.GetKey(KeyCode.E) && !gm.getGoldPicked())
            {
                pickTreasure();
            }

            if (transform.position.x < -40 && gm.getGoldPicked())
            {
                gm.setGameOver();
            }


        }
    }
    private void pickTreasure()
    {
        treasure = GameObject.FindWithTag("Treasure");
        float distance = Vector3.Distance(treasure.transform.position, transform.position);
        if (distance < 6)
        {
            gm.setGoldPicked(true);
            Destroy(treasure);
        }
        Debug.Log("distance: " + distance);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Barrier")
        {
            gm.setPlayerDead(true);
            gm.setGameOver();
            Destroy(gameObject);

        }
    }

}
