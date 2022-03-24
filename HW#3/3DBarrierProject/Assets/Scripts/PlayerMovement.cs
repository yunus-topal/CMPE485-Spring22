using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameObject manager;
    private GameManager gm;
    private GameObject treasure;
    private float speed = 50f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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
                //transform.position += new Vector3(1f * speed * Time.deltaTime, 0, 0);
                rb.AddForce(Vector3.right * Time.deltaTime * speed, ForceMode.Impulse);
            }
            if (Input.GetKey(KeyCode.S))
            {
                //transform.position += new Vector3(-1f * speed * Time.deltaTime, 0, 0);
                rb.AddForce(Vector3.left * Time.deltaTime * speed, ForceMode.Impulse);

            }

            if (Input.GetKey(KeyCode.E) && !gm.getGoldPicked())
            {
                pickTreasure();
            }

            if (transform.position.x < -40 && gm.getGoldPicked())
            {
                rb.velocity = new Vector3(0, 0, 0);
                gm.setGameOver();
            }
            if (transform.position.x > 45)
            {
                rb.velocity = new Vector3(0, 0, 0);
                transform.position = new Vector3(45f, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -45)
            {
                rb.velocity = new Vector3(0, 0, 0);
                transform.position = new Vector3(-45f, transform.position.y, transform.position.z);
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
