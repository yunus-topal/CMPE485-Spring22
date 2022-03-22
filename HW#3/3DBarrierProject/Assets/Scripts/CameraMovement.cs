using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool onTop = true;
    // 0 60 0 position
    // 90 0 0 rotation


    // -60 90 0
    // 60 90 0
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (onTop)
            {
                onTop = false;
                transform.position += new Vector3(-60f, 30f, 0);
                transform.rotation = Quaternion.Euler(60f, 90f, 0f);

                //transform.Rotate(-30f, 90f, 0f);
            }
            else
            {
                onTop = true;
                transform.position += new Vector3(60f, -30f, 0);
                transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                //transform.Rotate(30f, -90f, 0f);
            }
        }
    }
}
