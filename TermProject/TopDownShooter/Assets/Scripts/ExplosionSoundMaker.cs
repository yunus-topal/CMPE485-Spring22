using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundMaker : MonoBehaviour
{
    public AudioClip explosionClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(explosionClip);
    }
}
