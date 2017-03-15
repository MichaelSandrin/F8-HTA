using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPoints : MonoBehaviour {
	//respawnPoint = GameObject.Find("SpawnPoint").transform.position;
	void OnTriggerEnter(Collider Col) {
		// when the player hits this trigger change the respawn point for when the player dies to this point
		if (Col.tag == "Player") {            //makes a call to player variable call respawnPoint to record where respawn should occurGameObject.Find("Plume").GetComponent<CharacterMovement>().respawnPoint = transform.position + new Vector3(0, 2, 0); }
		}
	}
}