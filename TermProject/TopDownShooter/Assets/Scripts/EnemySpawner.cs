using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy),2.0f,1.0f);
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
