using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameOver = false;
    private bool bossPhase = false;
    public void SetGameOver(bool b)
    {
        gameOver = b;
    }

    public bool GetGameOver()
    {
        return gameOver;
    }

    public void SetBossPhase(bool b)
    {
        bossPhase = b;
    }

    public bool GetBossPhase()
    {
        return bossPhase;
    }
}
