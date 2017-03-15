using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {   
        Application.LoadLevel("Moving Platform Level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
