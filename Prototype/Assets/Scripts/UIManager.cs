using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameManager GM;
  

    // Use this for initialization
    // Update is called once per frame
    void Update()
    {
        ScanForKeyStroke();
    }

    void ScanForKeyStroke()
    {
        if ((Input.GetKeyDown("escape")) || Input.GetKeyDown("joystick button 7")) {
            GM.TogglePauseMenu();
        }
    }
 
}