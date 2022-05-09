using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] enemies;
    public GameObject effect;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),2.0f,2f);
    }

    void SpawnEnemy()
    {
        float xRandom = Random.Range(-50f, 50f);
        float zRandom = Random.Range(-25f,25f);
        int random = Random.Range(0,enemies.Length);
        StartCoroutine(EnemyRise(xRandom, zRandom, random));
    }

    IEnumerator EnemyRise(float x, float z, int type)
    {
        Instantiate(effect, new Vector3(x, 3f, z), Quaternion.Euler(new Vector3(-90, 0, 0)));
        yield return new WaitForSeconds(1f);
        GameObject enemy = Instantiate(enemies[type], new Vector3(x, 3f,z), Quaternion.identity );
        if (type < 2) enemy.GetComponent<EnemyMovement>().Initialize(mainCamera);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindWithTag("GameController").GetComponent<GameManager>().GetGameOver()  || GameObject.FindWithTag("GameController").GetComponent<GameManager>().GetBossPhase())
        {
            CancelInvoke(nameof(SpawnEnemy));
        }
        
    }
}
