using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButtonTriggers : MonoBehaviour {

    //Hanger
    public float currentLerpTime = 0;
    private float lerpTime = 8;
    GameObject hangerOne;
    public Vector3 hangerOneOP;
    public Vector3 hangerOneNP;
    public Vector3 hangerOneCP;

    GameObject hangerTwo;
    public Vector3 hangerTwoOP;
    public Vector3 hangerTwoNP;
    public Vector3 hangerTwoCP;

    GameObject hangerThree;
    public Vector3 hangerThreeOP;
    public Vector3 hangerThreeNP;
    public Vector3 hangerThreeCP;

    GameObject hangerFour;
    public Vector3 hangerFourOP;
    public Vector3 hangerFourNP;
    public Vector3 hangerFourCP;

    GameObject hangerFive;
    public Vector3 hangerFiveOP;
    public Vector3 hangerFiveNP;
    public Vector3 hangerFiveCP;

    GameObject hangerSix;
    public Vector3 hangerSixOP;
    public Vector3 hangerSixNP;
    public Vector3 hangerSixCP;

    GameObject hangerSeven;
    public Vector3 hangerSevenOP;
    public Vector3 hangerSevenNP;
    public Vector3 hangerSevenNP2;
    public Vector3 hangerSevenCP;

    // float speed = 50 * Time.deltaTime;
    float distance = 1.9f;
    Transform target;
    public bool triggerReset = false;
    public bool triggerButtonOne = false;
    public bool triggerButtonTwo = false;
    public bool triggerButtonThree = false;

    // Use this for initialization
    void Start () {
        hangerOne = GameObject.Find("hangingPlatform1");
        hangerOneOP = GameObject.Find("hangingPlatform1").transform.position;
        hangerOneNP = hangerOne.transform.position + Vector3.up * distance;

        hangerTwo = GameObject.Find("hangingPlatform2");
        hangerTwoOP = GameObject.Find("hangingPlatform2").transform.position;
        hangerTwoNP = hangerTwo.transform.position + Vector3.back * distance;

        hangerThree = GameObject.Find("hangingPlatform3");
        hangerThreeOP = GameObject.Find("hangingPlatform3").transform.position;
        hangerThreeNP = hangerThree.transform.position + Vector3.up * distance;

        hangerFour = GameObject.Find("hangingPlatform4");
        hangerFourOP = GameObject.Find("hangingPlatform4").transform.position;
        hangerFourNP = hangerFour.transform.position + Vector3.down * distance;

        hangerFive = GameObject.Find("hangingPlatform5");
        hangerFiveOP = GameObject.Find("hangingPlatform5").transform.position;
        hangerFiveNP = hangerFive.transform.position + Vector3.down * distance;

        hangerSix = GameObject.Find("hangingPlatform6");
        hangerSixOP = GameObject.Find("hangingPlatform6").transform.position;
        hangerSixNP = hangerSix.transform.position + Vector3.left * distance;

        hangerSeven = GameObject.Find("hangingPlatform7");
        hangerSevenOP = GameObject.Find("hangingPlatform7").transform.position;
        hangerSevenNP = hangerSeven.transform.position + Vector3.left * distance;
        hangerSevenNP2 = hangerSeven.transform.position + Vector3.left * distance;
    }
	
	// Update is called once per frame
	void Update () {
        hangerOneCP = GameObject.Find("hangingPlatform1").transform.position;
        hangerTwoCP = GameObject.Find("hangingPlatform2").transform.position;
        hangerThreeCP = GameObject.Find("hangingPlatform3").transform.position;
        hangerFourCP = GameObject.Find("hangingPlatform4").transform.position;
        hangerFiveCP = GameObject.Find("hangingPlatform5").transform.position;
        hangerSixCP = GameObject.Find("hangingPlatform6").transform.position;
        hangerSevenCP = GameObject.Find("hangingPlatform7").transform.position;
        moveHangerPlateForm();  

    }

    void moveHangerPlateForm()
    {
        if (triggerReset)
        {

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            hangerOne.transform.position = Vector3.Lerp(hangerOneCP, hangerOneOP, Perc);
            hangerTwo.transform.position = Vector3.Lerp(hangerTwoCP, hangerTwoOP, Perc);
            hangerThree.transform.position = Vector3.Lerp(hangerThreeCP, hangerThreeOP, Perc);
            hangerFour.transform.position = Vector3.Lerp(hangerFourCP, hangerFourOP, Perc);
            hangerSix.transform.position = Vector3.Lerp(hangerSixCP, hangerSixOP, Perc);
            hangerSeven.transform.position = Vector3.Lerp(hangerSevenCP, hangerSevenOP, Perc);

        }


        if (triggerButtonOne)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            hangerOne.transform.position = Vector3.Lerp(hangerOneCP, hangerOneNP, Perc);
            hangerThree.transform.position = Vector3.Lerp(hangerThreeCP, hangerThreeNP, Perc);
            hangerFour.transform.position = Vector3.Lerp(hangerFourCP, hangerFourNP, Perc);

        }

        if (triggerButtonTwo)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            //
            hangerSix.transform.position = Vector3.Lerp(hangerSixCP, hangerSixNP, Perc);

            //hangerFour.transform.position = Vector3.Lerp(hangerFourOP, hangerFourNP, Perc);
            //hangerSeven.transform.position = Vector3.Lerp(hangerSevenOP, hangerSevenNP, Perc);
        }

        if (triggerButtonThree)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                currentLerpTime = lerpTime;
            }
            float Perc = currentLerpTime / lerpTime;
            hangerTwo.transform.position = Vector3.Lerp(hangerTwoCP, hangerTwoNP, Perc);
            hangerSeven.transform.position = Vector3.Lerp(hangerSevenCP, hangerSevenNP, Perc);
        }

    }

    void OnTriggerEnter(Collider Col)
    {
        if (Col.tag == "Player" && gameObject.tag == "ButtonReset")
        {
            triggerReset = true;
            transform.localScale += new Vector3(0, 0.1f, 0);
            print("Reset Pressed");
        }
        if (Col.tag == "Player" && gameObject.tag == "ButtonOne")
        {

            triggerButtonOne = true;
            transform.localScale += new Vector3(0, 0.1f, 0);
        }
        if (Col.tag == "Player" && gameObject.tag == "ButtonTwo")
        {
            triggerButtonTwo = true;
            transform.localScale += new Vector3(0, 0.1f, 0);
        }
        if (Col.tag == "Player" && gameObject.tag == "ButtonThree")
        {
            triggerButtonThree = true;
            transform.localScale += new Vector3(0, 0.1f, 0);
        }
    }

    void OnTriggerExit(Collider Col)
    {
        triggerReset = false;
        triggerButtonOne = false;
        triggerButtonTwo = false;
        triggerButtonThree = false;
        
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
