using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushPower = 10.0f;
    Vector3 pushDir;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody objectBody = hit.collider.attachedRigidbody;

        if (objectBody == null || objectBody.isKinematic)
        {
            print("no object");
        }
        Debug.Log(hit.moveDirection);
        print(hit.moveDirection);
        /*
        if (hit.moveDirection)
        {
            pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
            objectBody.velocity = pushDir * pushPower;
        }*/

        


    }
}
