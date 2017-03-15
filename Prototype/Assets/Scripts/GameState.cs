using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
	public void SavePlayerPosition() {
		PlayerPrefs.SetFloat("Player", transform.position.x);
		PlayerPrefs.SetFloat("Player", transform.position.y);
		PlayerPrefs.SetFloat("Player", transform.position.z);


	}

	public void LoadCharacterPosition() {
		//Vector3 playerPosition = PlayerPrefs.set("Player");
		//transform.postion = playerPosition;
	}
}
