using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip walk;
    public AudioClip au_idle, au_walk;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // make sure that we have an AudioSource - do this here once instead of every frame
        if (audioSource == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            // let's just add the AudioSource component dynamically
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // make sure that object1 is a valid GameObject and has an animation - do this here once instead of every frame

    }


    void walkSound()
    {
        audioSource.PlayOneShot(au_walk);
    }
}