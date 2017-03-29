using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadGame : MonoBehaviour
{

    public string lastLevel;
    public string level;

    public void loadGame()
    {
        level = SaveLoadManager.Load();
        if(level == "Level 01(medit2)")
        {
            Application.LoadLevel("VideoTransition");
        }
        else if(level == "Level 02")
        {
            Application.LoadLevel("VideoTransition");
        }else
        {

        }
       // Application.LoadLevel(level);
    }


    void saveGame()
    {
        SaveLoadManager.Save(GameObject.Find("Plume").GetComponent<CharacterMovement>().currentLevel);
        Application.LoadLevel("SaveRoom");
    }

    void loadSaveRoom()
    {
        Application.LoadLevel("SaveRoom");
    }
 

    void OnTriggerStay(Collider Col)
    {
     

        if (Col.gameObject.tag == "Player" && GameObject.Find("Plume").GetComponent<CharacterMovement>().currentLevel != "SaveRoom")
        {
            saveGame();
        }else
        {
            loadGame();
        }
    }
}
