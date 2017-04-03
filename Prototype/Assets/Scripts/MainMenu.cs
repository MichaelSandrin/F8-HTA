using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public string level;
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
        level = SaveLoadManager.delete();
        Application.LoadLevel("VideoTransition");
    }

    public void Quit() {
        Application.Quit();
    }

    
}
