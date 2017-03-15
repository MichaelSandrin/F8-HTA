using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour {

    public float bounce =100f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision hit)
    {
        
        if (hit.gameObject.tag == "Player")
        {

            print("bounce2");

            GameObject.Find("Plume").GetComponent<CharacterMovement>().ChController.transform.position += (Vector3.up * bounce * Time.deltaTime);
                
        }
    }
}
