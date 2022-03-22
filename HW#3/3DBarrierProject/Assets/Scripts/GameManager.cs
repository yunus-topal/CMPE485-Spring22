using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject barrier1;
    public GameObject barrier2;
    public Button playButton;
    public Text resultText;
    private bool gameOver = true;
    private bool goldPicked = false;
    private bool playerDead = false;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playButton.onClick.AddListener(startGame);
    }

    // Update is called once per frame
    void Update()
    {
        // if (goldPicked && transform.position.x < -40)
        // {
        //     Debug.Log("You Won!");
        //     gameOver = true;
        //     playButton.gameObject.SetActive(true);
        //     resultText.text = "You Won!";
        //     resultText.gameObject.SetActive(true);

        // }
    }

    public void startGame()
    {
        Debug.Log("Game is starting");
        barrier1.GetComponent<BarrierMovement>().startBarriers();
        barrier2.GetComponent<BarrierMovement>().startBarriers();
        gameOver = false;
    }

    public bool getGameOver()
    {
        return gameOver;
    }
    public void setGameOver()
    {
        if (playerDead)
        {
            resultText.text = "You Lost!";
            resultText.color = Color.red;
        }
        else
        {
            resultText.text = "You Won!";
            resultText.color = Color.green;
        }
        resultText.gameObject.SetActive(true);
        barrier1.GetComponent<BarrierMovement>().stopBarriers();
        barrier2.GetComponent<BarrierMovement>().stopBarriers();

        gameOver = true;
    }
    public bool getGoldPicked()
    {
        return goldPicked;
    }
    public void setGoldPicked(bool value)
    {
        goldPicked = value;
    }

    public bool getPlayerDead()
    {
        return playerDead;
    }
    public void setPlayerDead(bool value)
    {
        playerDead = value;
    }


}
