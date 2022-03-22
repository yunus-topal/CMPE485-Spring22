using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Text diffButtonText;
    public Button diffButton;
    public Text diffText;
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
    private bool isHard = false;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(startGame);
        diffButton.onClick.AddListener(changeDifficulty);
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
        diffButton.gameObject.SetActive(false);
        diffText.gameObject.SetActive(false);
        audioSource.GetComponent<AudioHandler>().startAudio();

    }
    public void changeDifficulty()
    {
        //set to normal
        if (isHard)
        {
            barrier1.GetComponent<BarrierMovement>().setSpeed(100f);
            barrier2.GetComponent<BarrierMovement>().setSpeed(100f);
            barrier1.GetComponent<BarrierMovement>().setShorterDuration(1f);
            barrier1.GetComponent<BarrierMovement>().setLongerDuration(2f);
            barrier2.GetComponent<BarrierMovement>().setShorterDuration(1f);
            barrier2.GetComponent<BarrierMovement>().setLongerDuration(2f);
            diffButtonText.text = "NORMAL";
            isHard = false;

        }
        // set to hard
        else
        {
            barrier1.GetComponent<BarrierMovement>().setSpeed(500f);
            barrier2.GetComponent<BarrierMovement>().setSpeed(500f);
            barrier1.GetComponent<BarrierMovement>().setShorterDuration(0.3f);
            barrier1.GetComponent<BarrierMovement>().setLongerDuration(0.5f);
            barrier2.GetComponent<BarrierMovement>().setShorterDuration(0.3f);
            barrier2.GetComponent<BarrierMovement>().setLongerDuration(0.5f);
            diffButtonText.text = "HARD";
            isHard = true;
        }
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
        diffButton.gameObject.SetActive(true);
        diffText.gameObject.SetActive(true);
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
