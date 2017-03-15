using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {

	// Use this for initialization
	public void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("Player", transform.position.x);
        PlayerPrefs.SetFloat("Player", transform.position.y);
        PlayerPrefs.SetFloat("Player", transform.position.z);


    }
	
	// Update is called once per frame
	public void LoadCharacterPosition ()
    {
        //Vector3 playerPosition = PlayerPrefs.set("Player");

        //transform.postion = playerPosition;
	}
}
