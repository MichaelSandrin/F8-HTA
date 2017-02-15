using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }


    void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player")
        {
            //character.transform.position = respawnPoint2;
            //SceneManagement.SceneManager.LoadScene("Menu_Level");
            SceneManager.LoadScene ("LadderPuzzle");
        }
    }

}
