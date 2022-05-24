using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerScript : MonoBehaviour
{
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audioToPlay;
    public bool alreadyPlayed = false;
    void Start()
    {
        audioToPlay = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!alreadyPlayed)
        {
            audioToPlay.PlayOneShot(SoundToPlay,Volume);
            alreadyPlayed = true;
        }
    }
}
