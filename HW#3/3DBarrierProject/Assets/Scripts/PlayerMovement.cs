using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private GameManager gm;
    private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        gm = gameObject.GetComponent<GameManager>();
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
        }
    }
}
