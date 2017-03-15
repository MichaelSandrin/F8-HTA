using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour {
  
    public UIManager UI;
    public bool paused;
    GameObject pauseMenu;
    private Canvas CanvasObject;
    GameObject continueButton;
    
  

    void Start()
    {
        continueButton = GameObject.Find("Continue_Button");    
        CanvasObject = GameObject.Find("PauseCanvas").GetComponent<Canvas>();
        CanvasObject.GetComponent<Canvas>().enabled = true; //level starts paused to get around no controls on restart
    }

    public void TogglePauseMenu()
    {
        if (CanvasObject.enabled == true)
        {
            CanvasObject.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
        }
        else
        {
            CanvasObject.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0f;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(continueButton);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

}