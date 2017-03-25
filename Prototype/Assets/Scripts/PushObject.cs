using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushPower = 10.0f;
    Vector3 pushDir;

    //GameObject other = new GameObject("CubeWithTriggers");

    Rigidbody box;


    void Start()
    {
        box = GameObject.Find("CubeWithTriggers").GetComponent<Rigidbody>();
    }



    void OnTriggerStay(Collider Col)
    {

        /*
        if (Col.gameObject.tag == "Player" && GameObject.Find("North"))
        {
            //pushDir = new Vector3(GameObject.Find("Plume").GetComponent<CharacterMovement>().ChController.moveDirection.x, 0, GameObject.Find("Plume").GetComponent<CharacterMovement>().moveDirection.z);
            box.AddForce(transform.forward * pushPower);
        }
        else if (Col.gameObject.tag == "Player" && GameObject.Find("South"))
        {
            box.AddForce(-transform.forward * pushPower);
        }
        else if (Col.gameObject.tag == "Player" && GameObject.Find("East"))
        {
            box.AddForce(transform.right * pushPower);
        }
        else if (Col.gameObject.tag == "Player" && GameObject.Find("West"))
        {
            box.AddForce(-transform.right * pushPower);
        }*/
    }

    
}
