using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void startAudio()
    {
        audioSource.Play();
    }
    public void stopAudio()
    {
        audioSource.Stop();
    }
}
