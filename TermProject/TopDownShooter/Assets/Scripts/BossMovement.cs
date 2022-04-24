using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private GameObject gameManager;

    private Animator bossAnimator;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        bossAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnMinions()
    {
        
    }
    public IEnumerator StartEntrance()
    {
        float time = gameManager.GetComponent<GameManager>().GetBossTransition();
        for (float f = 0; f < time; f += Time.deltaTime)
        {
            float distance = 15f / time;
            transform.position -= new Vector3(0,0,distance * Time.deltaTime);
            yield return null;
        }
        bossAnimator.SetTrigger("on_place_trig");
        SpawnMinions();
    }
}
