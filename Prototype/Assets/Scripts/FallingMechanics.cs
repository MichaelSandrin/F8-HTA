using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMechanics : MonoBehaviour {

    public bool isFalling = false;
    public float fallSpeed = 0f;

    //Fucntion detects if the player enters the trigger to set the variable true
	void OnTriggerEnter( Collider collider)
    {
        if(collider.tag == "Player")
        {
            isFalling = true;    
        }
    }
	
	// Update is called once per frame
	void Update () {

        //If the variable isFall is true, the falling platform will continue to fall by time frame
        if (isFalling)
        {
            fallSpeed += Time.deltaTime/20;
            transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed, transform.position.z);
        }
	}
}
