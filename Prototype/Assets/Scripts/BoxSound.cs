using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BoxSound : MonoBehaviour {
    private AudioSource audioSource;
    public AudioClip boxPush;
    private bool isPushed;
    
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 1;
        // make sure that we have an AudioSource - do this here once instead of every frame
        if (audioSource == null)
        { // if AudioSource is missing
            Debug.LogWarning("AudioSource component missing from this gameobject. Adding one.");
            // let's just add the AudioSource component dynamically
            audioSource = gameObject.AddComponent<AudioSource>();
            isPushed = false;
        }

        Rigidbody player = GameObject.Find("Plume").GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        boxSound();
	}

   
    
    void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            print("push");
            isPushed = true;
        }

    }
    void OnCollisionStay(Collision collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            print("push");
            isPushed = true;
        }

    }
    void OnCollisionExit(Collision collision) {

        if (collision.gameObject.tag == "Player")
        {
            print("no push");
            isPushed = false;
        }
    }

    void boxSound()
    {
        if (isPushed == true)
        {
            audioSource.clip = boxPush;
            audioSource.Play();
            audioSource.loop = true;
           
        }
        else {

            audioSource.Stop();
        }
    }
}
