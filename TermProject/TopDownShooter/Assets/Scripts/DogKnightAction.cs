using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogKnightAction : MonoBehaviour
{
    private GameObject gameManager;
    public float coolDown = 0f;
    public GameObject attackPoint;
    private float attackRange = 5f;
    private float parryRange = 3f;
    public LayerMask enemyLayers;
    private Animator dogAnimator;
    private DogKnightMovement movementScript;
    private bool isOnRage = false;

    private Slider rageBar;
    private float hitPower = 1f;
    public AudioClip audioClip;
    public AudioClip swingClip;
    public AudioClip clankClip;
    AudioSource audioSource;

    public bool getRage()
    {
        return isOnRage;
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        Slider[] sliders = FindObjectsOfType<Slider>();
        foreach (Slider slider in sliders)
        {
            if (slider.gameObject.CompareTag("RageBar"))
            {
                rageBar = slider;
                break;
            }      
        }

        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = GameObject.FindWithTag("GameController");
        dogAnimator = gameObject.GetComponent<Animator>();
        movementScript = gameObject.GetComponent<DogKnightMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetComponent<GameManager>().GetGameOver())
        {
            // actions
            if (Input.GetKey(KeyCode.Mouse0) && !movementScript.GetDashing())
            {
                if (coolDown <= 0f)
                {
                    StartCoroutine(Attack());
                }
            }
            else if (Input.GetKey(KeyCode.Mouse1) && !movementScript.GetDashing())
            {
                if (coolDown <= 0f)
                {
                    StartCoroutine(Parry());
                }
            }
            else if (Input.GetKey(KeyCode.E) && !movementScript.GetDashing() && rageBar.value >= 1f && !isOnRage)
            {
                if (coolDown <= 0f)
                {
                    StartCoroutine(Rage());
                }
            }
        }
        
        if (coolDown > 0f)
        { 
            coolDown -= Time.deltaTime;
        }
    }

    public float GetCoolDown()
    {
        return coolDown;
    }

    IEnumerator Shrink()
    {
        float time = 1f;
        for (float f = 0; f < time; f += Time.deltaTime)
        {
            float decrease = (2f / time) * Time.deltaTime;
            transform.localScale -= new Vector3(decrease,decrease,decrease);
            attackRange -= decrease / 2;
            parryRange -= decrease / 2;

            yield return null;
        }
        isOnRage = false;
        hitPower = 1f;
    }
    IEnumerator Rage()
    {
        isOnRage = true;
        hitPower = 2f;
        dogAnimator.SetTrigger("rage_trig");
        coolDown = 1f;
        float time = 1f;
        audioSource.PlayOneShot(audioClip);
        for (float f = 0; f < time; f += Time.deltaTime)
        {
            float increase = (2f / time) * Time.deltaTime;
            transform.localScale += new Vector3(increase,increase,increase);
            attackRange += increase / 2;
            parryRange += increase / 2;
            yield return null;
        }

        StartCoroutine(RageDeplete());
    }

    IEnumerator RageDeplete()
    {
        while (rageBar.value >= 0.1f)
        {
            float barDecrease = Time.deltaTime / 3;
            rageBar.value -= barDecrease;
            yield return null;
        }

        StartCoroutine(Shrink());
    }
    IEnumerator Parry()
    {
        dogAnimator.SetTrigger("defend_trig");
        coolDown = 0.3f;
        yield return new WaitForSeconds(0.2f);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, parryRange, enemyLayers);
        foreach (Collider hitEnemy in hitEnemies)    
        {
            if (hitEnemy.gameObject.tag.Contains("Bullet"))
            {
                Destroy(hitEnemy.gameObject);
                rageBar.value += 0.2f;
            }
        }
    }
    IEnumerator Attack()
    {
        dogAnimator.SetTrigger("attack_trig");
        coolDown = 0.3f;
        yield return new WaitForSeconds(0.2f);
        AudioClip attack = swingClip;
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, attackRange, enemyLayers);
        foreach (Collider hitEnemy in hitEnemies)
        {
            rageBar.value += 0.05f;
            if (hitEnemy.gameObject.CompareTag("SkeletonEnemy") || hitEnemy.gameObject.CompareTag("KnightEnemy"))
            {
                hitEnemy.gameObject.GetComponent<SkeletonEnemyMovement>().TakeDamage(hitPower);
                attack = clankClip;
            }
            else if (hitEnemy.gameObject.tag.Contains("Enemy"))
            {
                hitEnemy.gameObject.GetComponent<EnemyMovement>().TakeDamage(hitPower);
            }
            else if (hitEnemy.gameObject.tag.Contains("Boss"))
            {
                hitEnemy.gameObject.GetComponent<BossMovement>().TakeDamage(hitPower);
            }
        }
        audioSource.PlayOneShot(attack);

    }
}
