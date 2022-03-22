using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMovement : MonoBehaviour
{
    private IEnumerator coroutine;
    public float speed = 100f;
    private Rigidbody rb;
    private float direction = 1f;
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        coroutine = Move();
    }
    public void startBarriers()
    {
        StartCoroutine(coroutine);
    }
    public void stopBarriers()
    {
        StopCoroutine(coroutine);
        rb.velocity = new Vector3(0, 0, 0);
    }

    // every 2 seconds perform the print()
    private IEnumerator Move()
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

}
