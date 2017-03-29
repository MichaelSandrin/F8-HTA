using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class VideoSceneTransition : MonoBehaviour {

    public MovieTexture movie;
    private Game game;
    public string level;

    // Use this for initialization
    void Start () {
        level = SaveLoadManager.Load();
        print(level);
        if (level == null)
        {
            movie = (MovieTexture)Resources.Load("OpeningCutscene26-3-2017", typeof(MovieTexture));
        }else if(level == "Level 01(medit2)")
        {
            movie = (MovieTexture)Resources.Load("Title", typeof(MovieTexture));
        }
        else if (level == "Level 02")
        {
            movie = (MovieTexture)Resources.Load("Title", typeof(MovieTexture));
        }

        this.GetComponent<Renderer>().material.mainTexture = movie as MovieTexture;
        this.GetComponent<AudioSource>().clip = movie.audioClip;
        movie.Play();
        this.GetComponent<AudioSource>().Play();

    }
	
	// Update is called once per frame
	void Update () {

        if ((!movie.isPlaying && level == null))
        {
            Application.LoadLevel("Level 01(medit2)");
        }

        if ((!movie.isPlaying && level == "Level 01(medit2)"))
        {
            Application.LoadLevel("Level 02");
        }else if ((!movie.isPlaying && level == "Level 02"))
        {
            Application.LoadLevel("Level_03");
        }
    }
}
