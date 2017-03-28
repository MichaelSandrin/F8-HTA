using System.Collections;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]

public class TitleMenu : MonoBehaviour {

    public MovieTexture movie;

	// Use this for initialization
	void Start () {

        GetComponent<Renderer>().material.mainTexture = movie as MovieTexture;
        GetComponent<AudioSource>().clip = movie.audioClip;
        movie.Play();
        movie.loop = true;
        GetComponent<AudioSource>().Play();
		
	}





}
