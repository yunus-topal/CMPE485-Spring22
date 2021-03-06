using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private GameObject gameManager;

    private Animator bossAnimator;

    private float hp = 25f;

    public GameObject skeletonPrefab;

    public GameObject skeletenEffect;
    public AudioClip audioClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        gameManager = GameObject.FindWithTag("GameController");
        bossAnimator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private IEnumerator SpawnMinions()
    {
        yield return new WaitForSeconds(1f);
        bossAnimator.SetTrigger("spawn_trig");
        yield return new WaitForSeconds(1f);
        Instantiate(skeletenEffect, new Vector3(-40f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        Instantiate(skeletenEffect, new Vector3(-20f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        Instantiate(skeletenEffect, new Vector3(40f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        Instantiate(skeletenEffect, new Vector3(20f, 3f, 25f), Quaternion.Euler(new Vector3(-90, 0, 0)));
        for (int i = 0; i < 5; i++)
        {
            bossAnimator.SetTrigger("spawn_trig");
            yield return new WaitForSeconds(1f);
            audioSource.PlayOneShot(audioClip);
            SpawnSkeletons();
            yield return new WaitForSeconds(1f);
        }

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
        StartCoroutine(AttackPattern());
    }

    private IEnumerator AttackPattern()
    {
        while (!gameManager.GetComponent<GameManager>().GetGameOver())
        {
            StartCoroutine(SpawnMinions());
            yield return new WaitForSeconds(5f);
            gameManager.GetComponent<GameManager>().StartSpawner();
            yield return new WaitForSeconds(10f);
        }
    }
    private IEnumerator DestroySelf()
    {
        StopCoroutine(SpawnMinions());
        bossAnimator.SetTrigger("die_trig");
        yield return new WaitForSeconds(1f);
        gameManager.GetComponent<GameManager>().SetGameOver(true);
        gameManager.GetComponent<GameManager>().playVictory();
        Destroy(gameObject);
    }
    
    public void TakeDamage(float f)
    {
        hp -= f;
        if (hp <= 0)
        {
            StartCoroutine(DestroySelf());
            gameManager.GetComponent<GameManager>().IncreaseScore(tag);
        }
    }
}
