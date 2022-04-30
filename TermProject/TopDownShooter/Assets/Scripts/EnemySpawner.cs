using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),2.0f,1.5f);
    }

    void SpawnEnemy()
    {
        float xRandom = Random.Range(-20f, 20f);
        int random = Random.Range(0,enemies.Length);
        GameObject enemy = Instantiate(enemies[random], new Vector3(xRandom, 3f,mainCamera.transform.position.z + 25f), Quaternion.identity);
        if (random < 2) enemy.GetComponent<EnemyMovement>().Initialize(mainCamera);
        
        
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
