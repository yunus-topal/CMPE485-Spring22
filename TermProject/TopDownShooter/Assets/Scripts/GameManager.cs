using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool gameOver = true;
    private bool bossPhase = false;
    private float bossTransition = 5.0f;
    private int score = 0;
    private const int BossTrigScore = 10;
    public GameObject spawnManager;
    public Slider hpBar;
    public Slider skillBar;
    public GameObject playerPrefab;
    public GameObject bossPrefab;
    public Button startButton;

    private void Update()
    {
        if (bossPhase && bossTransition >= 0)
        {
            bossTransition -= Time.deltaTime;
        }
    }
    public void SetGameOver(bool b)
    {
        gameOver = b;
        if (gameOver)
        {
            StopSpawner();
            ShowUI();
        }
        else
        {
            InitializeGame();
        }
    }
    // show start button again
    private void ShowUI()
    {
        startButton.gameObject.SetActive(true);
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void SetBossPhase()
    {
        bossPhase = true;
        StopSpawner();
        BossMovement bossMover = GameObject.FindWithTag("Boss").GetComponent<BossMovement>();
        bossMover.StartCoroutine(bossMover.StartEntrance());
    }

    public void StopSpawner()
    {
        spawnManager.GetComponent<EnemySpawner>().CancelInvoke("SpawnEnemy");
    }

    public void StartSpawner()
    {
        spawnManager.GetComponent<EnemySpawner>().InvokeRepeating("SpawnEnemy",2.0f,2f);
    }
    
    public bool GetBossPhase()
    {
        return bossPhase;
    }

    public float GetBossTransition()
    {
        return bossTransition;
    }

    public void IncreaseScore(String objectType)
    {
        switch (objectType)
        {
            case "SkeletonEnemy": 
                score += 2;
                break;
            case "DefaultEnemy": 
                score += 3;
                break;
            case "LockedinEnemy": 
                score += 5;
                break;
            default: 
                Debug.Log("Unknown Object");
                break;
        }

        if (score > BossTrigScore && !bossPhase)
        {
            SetBossPhase();
        }
    }

    public void InitializeGame()
    {
        gameOver = false;
        score = 0;
        bossTransition = 5.0f;
        bossPhase = false;
        StartSpawner();
        // destroy player, boss and common enemies and bullets
        string[] tags = {"Player", "DefaultEnemy","LockedinEnemy", "KnightEnemy", "SkeletonEnemy","LockedinBullet","DefaultBullet","Boss","SkeletonSummon" };
        foreach (string objectTag in tags)
        {
            GameObject[] clones = GameObject.FindGameObjectsWithTag(objectTag);
            foreach (GameObject clone in clones)
            {
                Destroy(clone);
            }
        }
        // recreate player
        GameObject doggy = Instantiate(playerPrefab, new Vector3(0, 1, -20), Quaternion.identity);
        doggy.GetComponent<DogKnightAction>().Initialize();
        doggy.GetComponent<DogKnightMovement>().Initialize();
        // recreate boss with initial settings
        GameObject bossy = Instantiate(bossPrefab, new Vector3(0, 0, 50), Quaternion.Euler(0f, 180f, 0f));
        bossy.GetComponent<BossMovement>().Initialize();
        // adjust hp and skill bars
        hpBar.value = 1;
        skillBar.value = 0;

    }
}
