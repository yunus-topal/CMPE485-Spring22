using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button gameOverButton;
    private bool gameOver = false;
    private bool goldPicked = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (goldPicked && transform.position.x < -40)
        {
            Debug.Log("You Won!");
            gameOver = true;
            gameOverButton.gameObject.SetActive(true);

        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Gold")
        {
            goldPicked = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Barrier")
        {
            gameOver = true;
            Destroy(gameObject);
        }
    }

    public bool getGameOver()
    {
        return gameOver;
    }
}
