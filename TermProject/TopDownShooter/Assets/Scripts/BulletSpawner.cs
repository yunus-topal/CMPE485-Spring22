using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private float coolDown = 0f;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (coolDown <= 0f)
            {
                GameObject bullet = Instantiate(bulletPrefab,new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z ), Quaternion.identity);
                bullet.GetComponent<BulletMovement>().SetAngle(transform.eulerAngles.y);
                coolDown = 0.1f;
            }
        }
        if (coolDown > 0f)
        { 
            coolDown -= Time.deltaTime;
        }
    }
}
