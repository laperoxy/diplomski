using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTriggerScript : MonoBehaviour
{
    public AudioClip SoundToPlay;
    public float Volume;
    private AudioSource audio;
    public bool alreadyPlayed = false;
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!alreadyPlayed)
        {
            audio.PlayOneShot(SoundToPlay,Volume);
            alreadyPlayed = true;
        }
    }
}
