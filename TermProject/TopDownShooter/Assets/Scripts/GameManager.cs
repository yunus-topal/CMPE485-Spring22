using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    private bool bossPhase = false;
    private float bossTransition = 5.0f;
    private int score = 0;
    
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
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void SetBossPhase()
    {
        bossPhase = true;
        BossMovement bossMover = GameObject.FindWithTag("Boss").GetComponent<BossMovement>();
        bossMover.StartCoroutine(bossMover.StartEntrance());
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

        if (score > 10 && !bossPhase)
        {
            SetBossPhase();
        }
    }

}
