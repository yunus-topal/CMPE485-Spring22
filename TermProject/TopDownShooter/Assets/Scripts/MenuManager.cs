using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
    }

    public void StartGame()
    {
        gameManager.GetComponent<GameManager>().SetGameOver(false);
        gameObject.SetActive(false);
    }
}
