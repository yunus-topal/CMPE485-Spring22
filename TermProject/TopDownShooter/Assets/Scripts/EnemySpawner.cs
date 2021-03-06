using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] enemies;
    public GameObject[] effect;

    void SpawnEnemy()
    {
        // to make sure that x position will between -50, -10 or 10, 50.
        float xRandom = Random.Range(10f, 50f);
        if (Random.Range(0, 2) == 0) xRandom *= -1;
        float zRandom = Random.Range(-25f,25f);
        int random = Random.Range(0,enemies.Length);
        StartCoroutine(EnemyRise(xRandom, zRandom, random));
    }

    IEnumerator EnemyRise(float x, float z, int type)
    {
        Instantiate(effect[type], new Vector3(x, 3f, z), Quaternion.Euler(new Vector3(-90, 0, 0)));
        yield return new WaitForSeconds(1f);
        GameObject enemy = Instantiate(enemies[type], new Vector3(x, 3f,z), Quaternion.identity );
        if (type < 2) enemy.GetComponent<EnemyMovement>().Initialize(mainCamera);
    }
}
