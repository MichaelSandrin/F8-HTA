using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushPower = 10.0f;
    Vector3 pushDir;

    //GameObject other = new GameObject("CubeWithTriggers");
    /*
    Rigidbody box = other.GetComponent<Rigidbody>();

    void OnTriggerStay(Collider Col)
    {
   

        if (Col.gameObject.tag == "Player")
        {
            pushDir = new Vector3(col.moveDirection.x, 0, col.moveDirection.z);
        }
       
    }*/

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody objectBody = hit.collider.attachedRigidbody;

        if (objectBody == null || objectBody.isKinematic)
        {
            print("no object");
        }
        Debug.Log(hit.moveDirection);
        print(hit.moveDirection);
        
        
        
            pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            objectBody.velocity = pushDir * pushPower;
        
        
        


    }
}
