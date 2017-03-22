using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateMechanics : MonoBehaviour
{

    GameObject Gate;
    public Vector3 GateOP;
    public Vector3 GateNP;
    public Vector3 GateCP;

    public float currentLerpTime = 0;
    private float lerpTime = 8;

    // Use this for initialization
    void Start()
    {
        Gate = GameObject.Find("Gate");
        GateOP = GameObject.Find("Gate").transform.position;
        GateNP = Gate.transform.position + Vector3.up * 3f;
    }

    // Update is called once per frame
    void Update()
    {
        GateCP = GameObject.Find("Gate").transform.position;
        closeGate();

    }

    void openGate()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float Perc = currentLerpTime / lerpTime;
        Gate.transform.position = Vector3.Lerp(GateCP, GateNP, Perc / 30);
    }
    
    void closeGate()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float Perc = currentLerpTime / lerpTime;
        Gate.transform.position = Vector3.Lerp(GateCP, GateOP, Perc / 30);
    }

    void OnTriggerStay(Collider Col)
    {
        if (Col.tag == "Player")
        {
            openGate();
        }
    }

 
}