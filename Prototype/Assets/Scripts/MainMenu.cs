using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {
	void Start() {
		
	}

	void Update() {
		
	}

	public void StartGame() {   
		Application.LoadLevel("Moving Platform Level");
	}

	public void Quit() {
		Application.Quit();
	}
}
