using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject audioSource;
    public GameObject goldPrefab;
    public GameObject playerPrefab;
    public GameObject barrier1;
    public GameObject barrier2;
    public Button playButton;
    public Text buttonText;
    public Text resultText;
    private bool gameOver = true;
    private bool goldPicked = true;
    private bool playerDead = true;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(startGame);
    }

    public void startGame()
    {
        // reinitialize variables
        gameOver = false;
        if (goldPicked)
        {
            Instantiate(goldPrefab, new Vector3(0, 1f, 0), Quaternion.identity);
            goldPicked = false;
        }

        if (playerDead)
        {
            Instantiate(playerPrefab, new Vector3(-40f, 2.5f, 0), Quaternion.identity);
            player = GameObject.FindWithTag("Player");
            playerDead = false;
        }
        barrier1.GetComponent<BarrierMovement>().startBarriers();
        barrier2.GetComponent<BarrierMovement>().startBarriers();

        playButton.gameObject.SetActive(false);
        resultText.gameObject.SetActive(false);
        audioSource.GetComponent<AudioHandler>().startAudio();

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

        barrier1.GetComponent<BarrierMovement>().stopBarriers();
        barrier2.GetComponent<BarrierMovement>().stopBarriers();

        gameOver = true;
        buttonText.text = "PLAY AGAIN";
        resultText.gameObject.SetActive(true);
        playButton.gameObject.SetActive(true);
        audioSource.GetComponent<AudioHandler>().startAudio();

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
