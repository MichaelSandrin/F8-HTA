using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    void Start() {

    }

    void Update() {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
            print("Yo");
        }
    }

    public void StartGame() {
        Application.LoadLevel("Moving Platform Level");
    }

    public void Quit() {
        Application.Quit();
    }

    
}
