using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoButtonScript : MonoBehaviour {


    public float currentLerpTime = 0;
    private float lerpTime = 8;

    //Gates
    GameObject gateA;
    public Vector3 gateAOP;
    public Vector3 gateANP;
    public Vector3 gateACP;

    GameObject gateB;
    public Vector3 gateBOP;
    public Vector3 gateBNP;
    public Vector3 gateBCP;

    GameObject movingPlatform;
    public Vector3 movingPlatformOP;
    public Vector3 movingPlatformNP;
    public Vector3 movingPlatformCP;


    //Boolean for activation
    float distance = 16.5f;
    public bool triggerBridge = false;
    public bool triggerA = false;
    public bool triggerB = false;
    public bool changeA = false;
    public bool changeB = false;
    public bool changeSeqA = false;
    public bool changeSeqB = false;
    public bool changeSeqC = false;
    public bool changeSeqD = false;
    public bool changePlatform = false;

    public bool interact = false;

    public bool triggerLiftA = false;
    public bool triggerLiftB = false;

    // Use this for initialization
    void Start () {
        gateA = GameObject.Find("GateA");
        gateAOP = GameObject.Find("GateA").transform.position;
        gateANP = gateA.transform.position + Vector3.up * 5f;

        gateB = GameObject.Find("GateB");
        gateBOP = GameObject.Find("GateB").transform.position;
        gateBNP = gateB.transform.position + Vector3.up * 5f;

        /*
        movingPlatform = GameObject.Find("Moving Platform");
        movingPlatformOP = GameObject.Find("Moving Platform").transform.position;
        movingPlatformNP = movingPlatform.transform.position + Vector3.forward * 7f;
        movingPlatformNP = movingPlatform.transform.position + Vector3.back * 7f;
        */

    }
	
	// Update is called once per frame
	void Update () {
        gateACP = GameObject.Find("GateA").transform.position;
        gateBCP = GameObject.Find("GateB").transform.position;
        /*
        movingPlatformCP = GameObject.Find("movingPlatform").transform.position;
        */



        interaction();
        openGate();
	}

    void openGate()
    {
        if (triggerA && interact)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            gateA.transform.position = Vector3.Lerp(gateACP, gateANP, Perc / 30);
        }

        if (triggerA)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            gateB.transform.position = Vector3.Lerp(gateBCP, gateBNP, Perc / 30);
        }
    }

    void interaction()
    {
        if (Input.GetKey("f") || Input.GetKey("joystick button 2"))
        {
            interact = true;
        }
        else
        {
            interact = false;
        }

    }

    void OnTriggerStay(Collider Col)
    {
        //Player activat
        if ((Col.tag == "Player" && (gameObject.tag == ("TriggerA")) && interact))
        {
            triggerA = true;
        }

        if ((Col.tag == "Player" && gameObject.tag == ("ButtonBBridge")) && interact)
        {
            triggerB = true;

        }

        if ((Col.tag == "BoxPush" && gameObject.tag == ("TriggerA")))
        {
            triggerA = true;
        }

        if ((Col.tag == "Player" && gameObject.tag == ("TriggerLiftB")) && interact)
        {
            triggerLiftB = true;
        }
    }

    void OnTriggerExit(Collider Col)
    {
       
        triggerA = false;
        triggerB = false;
        triggerLiftA = false;
        triggerLiftB = false;
        triggerBridge = false;

        if (currentLerpTime == lerpTime)
        {
            currentLerpTime = 0;
        }
        else
        {
            currentLerpTime = 0;
        }
    }
}
