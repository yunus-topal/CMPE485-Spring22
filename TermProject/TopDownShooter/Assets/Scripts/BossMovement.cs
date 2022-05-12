using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private GameObject gameManager;

    private Animator bossAnimator;

    private int hp = 5;

    public GameObject skeletonPrefab;

    public GameObject skeletenEffect;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        bossAnimator = gameObject.GetComponent<Animator>();
    }

    private IEnumerator SpawnMinions()
    {
        yield return new WaitForSeconds(1f);
        bossAnimator.SetTrigger("spawn_trig");
        yield return new WaitForSeconds(1f);
        GameObject c1 = Instantiate(skeletenEffect, new Vector3(-40f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        GameObject c2 = Instantiate(skeletenEffect, new Vector3(-20f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        GameObject c3 = Instantiate(skeletenEffect, new Vector3(40f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        GameObject c4 = Instantiate(skeletenEffect, new Vector3(20f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        for (int i = 0; i < 5; i++)
        {
            SpawnSkeletons();
            yield return new WaitForSeconds(1.5f);
        }
        Destroy(c1);
        Destroy(c2);
        Destroy(c3);
        Destroy(c4);
    }

    
    
    private void SpawnSkeletons()
    {
        //spawn 2 skeletons on each side of the boss
        Instantiate(skeletonPrefab,new Vector3(-40f,3f,25f),Quaternion.identity);
        Instantiate(skeletonPrefab,new Vector3(-20f,3f,25f),Quaternion.identity);
        Instantiate(skeletonPrefab,new Vector3(40f,3f,25f),Quaternion.identity);
        Instantiate(skeletonPrefab,new Vector3(20f,3f,25f),Quaternion.identity);
    }
    public IEnumerator StartEntrance()
    {
        float time = gameManager.GetComponent<GameManager>().GetBossTransition();
        for (float f = 0; f < time; f += Time.deltaTime)
        {
            float distance = 25f / time;
            transform.position -= new Vector3(0,0,distance * Time.deltaTime);
            yield return null;
        }
        bossAnimator.SetTrigger("on_place_trig");
        StartCoroutine(SpawnMinions());
    }
    
    private IEnumerator DestroySelf()
    {
        StopCoroutine(SpawnMinions());
        bossAnimator.SetTrigger("die_trig");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
    
    public void TakeDamage()
    {
        hp -= 1;
        if (hp <= 0)
        {
            StartCoroutine(DestroySelf());
            gameManager.GetComponent<GameManager>().IncreaseScore(tag);
        }
    }
}
