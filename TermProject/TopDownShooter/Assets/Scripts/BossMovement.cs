using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private GameObject gameManager;

    private Animator bossAnimator;

    private bool isAlive = true;

    public GameObject skeletonPrefab;
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

    private IEnumerator SpawnMinions()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(1f);
            bossAnimator.SetTrigger("spawn_trig");
            yield return new WaitForSeconds(1f);
            SpawnSkeletons();
            yield return new WaitForSeconds(3f);
        }
    }

    private void SpawnSkeletons()
    {
        //spawn 2 skeletons
        Instantiate(skeletonPrefab,new Vector3(-10,0,100f),Quaternion.identity);
        Instantiate(skeletonPrefab,new Vector3(10,0,100f),Quaternion.identity);
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
        StartCoroutine(SpawnMinions());
    }
}
