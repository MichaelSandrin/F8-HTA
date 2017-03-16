using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fanWind : MonoBehaviour
{

	public float windForce = 5.0f;
	// Use this for initialization

	Vector3 fanRotation;
	//fan base rotation values
	bool groundCheck;
	//a boolean that checks if Plume is on the ground
    
	void Start ()
	{
		//initializes variables
		fanRotation = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		groundCheck = GameObject.Find ("Plume").GetComponent<CharacterMovement> ().Grounded ();
	}

	void OnTriggerStay (Collider Col)
	{
		//while in the trigger

		if (Col.tag == "Player") {
			//if the player is in the trigger make glide timer not decrease
			GameObject.Find ("Plume").GetComponent<CharacterMovement> ().glideEndurance -= 0;

			//if the fan is in certain orientations, push the player in corresponding direction
			if (fanRotation.x == 0 && fanRotation.y == 0 && fanRotation.z == 0) {
				GameObject.Find ("Plume").GetComponent<CharacterMovement> ().ChController.transform.position += (Vector3.up * windForce * Time.deltaTime);
			}

			if (fanRotation.x == 90 && fanRotation.y == 0 && fanRotation.z == 0) {
				GameObject.Find ("Plume").GetComponent<CharacterMovement> ().ChController.transform.position += (Vector3.forward * windForce * Time.deltaTime);
			}

			if (fanRotation.x == 180 && fanRotation.y == 0 && fanRotation.z == 0 && groundCheck != true) { //down not working for some reason
				GameObject.Find ("Plume").GetComponent<CharacterMovement> ().ChController.transform.position += (-Vector3.up * windForce * Time.deltaTime);
			}

			if (fanRotation.x == 270 && fanRotation.y == 0 && fanRotation.z == 0) {
				GameObject.Find ("Plume").GetComponent<CharacterMovement> ().ChController.transform.position += (-Vector3.forward * windForce * Time.deltaTime);
			}
			if (fanRotation.x == 0 && fanRotation.y == 0 && fanRotation.z == 90) {
				GameObject.Find ("Plume").GetComponent<CharacterMovement> ().ChController.transform.position += (-Vector3.right * windForce * Time.deltaTime);
			}

			if (fanRotation.x == 0 && fanRotation.y == 0 && fanRotation.z == 180 && groundCheck != true) {
				GameObject.Find ("Plume").GetComponent<CharacterMovement> ().ChController.transform.position += (-Vector3.up * windForce * Time.deltaTime);
			}

			if (fanRotation.x == 0 && fanRotation.y == 0 && fanRotation.z == 270) {
				GameObject.Find ("Plume").GetComponent<CharacterMovement> ().ChController.transform.position += (Vector3.right * windForce * Time.deltaTime);
			}
		}
	}

	void OnTriggerExit (Collider Col)
	{
		//when the player exits the collider make glideLockoutTimer be able to decrease.
		if (Col.tag == "Player") {
			GameObject.Find ("Plume").GetComponent<CharacterMovement> ().glideEndurance -= .25f;

		}
	}
}

