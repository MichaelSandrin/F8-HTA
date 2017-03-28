using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class VideoSceneTransition : MonoBehaviour {

    public MovieTexture movie;

    // Use this for initialization
    void Start () {
        //if (GameObject.Find("Plume").GetComponent<CharacterMovement>().currentLevel == null)
        //{
            GetComponent<Renderer>().material.mainTexture = movie as MovieTexture;
            GetComponent<AudioSource>().clip = movie.audioClip;
            movie.Play();
            
            GetComponent<AudioSource>().Play();
        
            
        //}
        //
    }
	
	// Update is called once per frame
	void Update () {
        if (!movie.isPlaying)
        {
            Application.LoadLevel("Level 01(medit2)");
        }
    }
}
