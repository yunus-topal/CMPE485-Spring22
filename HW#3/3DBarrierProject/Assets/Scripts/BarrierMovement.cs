using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMovement : MonoBehaviour
{

    public float speed = 100f;
    private Rigidbody rb;
    private float direction = 1f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(Move(2.0f));
    }
    // Update is called once per frame
    void Update()
    {

    }

    // every 2 seconds perform the print()
    private IEnumerator Move(float waitTime)
    {
        while (true)
        {
            while (transform.position.z < 10)
            {
                rb.AddForce(Vector3.forward * direction * Time.deltaTime * speed, ForceMode.Impulse);
                yield return null;
            }
            rb.velocity = new Vector3(0, 0, 0);
            direction = -1f;

            float duration = Random.Range(1f, 2f);
            yield return new WaitForSeconds(duration);

            while (transform.position.z > -10)
            {
                rb.AddForce(Vector3.forward * direction * Time.deltaTime * speed, ForceMode.Impulse);
                yield return null;
            }
            rb.velocity = new Vector3(0, 0, 0);
            direction = 1f;

            duration = Random.Range(1f, 2f);
            yield return new WaitForSeconds(duration);
        }
    }

    // without coroutines
    // void Move()
    // {
    //     if (transform.position.z > 10)
    //     {
    //         direction = -1f;
    //         rb.velocity = new Vector3(0, 0, 0);
    //     }
    //     if (transform.position.z < -10)
    //     {
    //         direction = 1f;
    //         rb.velocity = new Vector3(0, 0, 0);
    //     }

    //     rb.AddForce(Vector3.forward * direction * Time.deltaTime * speed, ForceMode.Impulse);

    // }

}
