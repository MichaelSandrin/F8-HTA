using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBlade : MonoBehaviour {

    public int fanSpeed = 120; //speed of rotation

    void Update()
    {

        //rotate around the local Y axis
        transform.Rotate(Vector3.up* fanSpeed * Time.deltaTime, Space.Self);
    }
}
