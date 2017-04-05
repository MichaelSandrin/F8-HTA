using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderFacing : MonoBehaviour
{

   
    // Use this for initialization

    Vector3 ladderRotation;
    //fan base rotation values
    
    //a boolean that checks if Plume is on the ground

    void Start()
    {
        //initializes variables
        ladderRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void OnTriggerStay(Collider Col)
    {
        //while in the trigger

        if (Col.tag == "Player")
        {
            //if the player is in the trigger make glide timer not decrease
            GameObject.Find("Plume").GetComponent<CharacterMovement>().glideEndurance -= 0;

            //if the fan is in certain orientations, push the player in corresponding direction
            if (ladderRotation.y == 0)
            {
                GameObject.Find("Plume").GetComponent<CharacterMovement>().ladderRotate = new Quaternion(0,180,0,transform.rotation.w);
            }         
        }
    }

    void OnTriggerExit(Collider Col)
    {
        //when the player exits the collider make glideLockoutTimer be able to decrease.
        if (Col.tag == "Player")
        {
            GameObject.Find("Plume").GetComponent<CharacterMovement>().glideEndurance -= .25f;

        }
    }
}


