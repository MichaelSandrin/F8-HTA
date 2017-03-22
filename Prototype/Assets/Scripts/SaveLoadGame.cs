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
        Application.LoadLevel(level);
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
     

        if (Col.gameObject.tag == "Player" && level != "SaveRoom")
        {
            saveGame();
        }else
        {
            loadGame();
        }
    }
}
