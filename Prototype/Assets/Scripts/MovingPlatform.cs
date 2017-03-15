using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	//Platform
	public float currentLerpTime = 0;
	private float lerpTime = 8;


	//Continuous Timer for pushers, bridges and lifts
	public float pusherCurrentLerpTime = 0;
	private float pusherLerpTime = 1f;

	public float pusherBCurrentLerpTime = 0;
	private float pusherBLerpTime = 3f;

	public float SeqACurrentLerpTime = 0;
	private float seqALerpTime = 1f;

	public float SeqBCurrentLerpTime = 0;
	private float seqBLerpTime = 2f;

	public float SeqCCurrentLerpTime = 0;
	private float seqCLerpTime = 3f;

	public float SeqDCurrentLerpTime = 0;
	private float seqDLerpTime = 5f;

	public float SeqECurrentLerpTime = 0;
	private float seqELerpTime = 5f;

	public float SeqFCurrentLerpTime = 0;
	private float seqFLerpTime = 6f;

	public float plateCurrentLerpTime = 0;
	private float platLerpTime = 3f;

	//Bridges
	GameObject bridgeOne;
	public Vector3 bridgeOneOP;
	public Vector3 bridgeOneNP;
	public Vector3 bridgeOneCP;

	GameObject bridgeTwo;
	public Vector3 bridgeTwoOP;
	public Vector3 bridgeTwoNP;
	public Vector3 bridgeTwoCP;

	GameObject bridgeThree;
	public Vector3 bridgeThreeOP;
	public Vector3 bridgeThreeNP;
	public Vector3 bridgeThreeCP;

	//Pushers
	GameObject pusher;
	public Vector3 pusherOP;
	public Vector3 pusherNP;
	public Vector3 pusherNP2;
	public Vector3 pusherCP;

	GameObject pusherB;
	public Vector3 pusherBOP;
	public Vector3 pusherBNP;
	public Vector3 pusherBNP2;
	public Vector3 pusherBCP;

	GameObject pusherC;
	public Vector3 pusherCOP;
	public Vector3 pusherCNP;
	public Vector3 pusherCNP2;
	public Vector3 pusherCCP;

	GameObject pusherD;
	public Vector3 pusherDOP;
	public Vector3 pusherDNP;
	public Vector3 pusherDNP2;
	public Vector3 pusherDCP;

	GameObject pusherE;
	public Vector3 pusherEOP;
	public Vector3 pusherENP;
	public Vector3 pusherENP2;
	public Vector3 pusherECP;

	GameObject pusherF;
	public Vector3 pusherFOP;
	public Vector3 pusherFNP;
	public Vector3 pusherFNP2;
	public Vector3 pusherFCP;

	GameObject pusherG;
	public Vector3 pusherGOP;
	public Vector3 pusherGNP;
	public Vector3 pusherGNP2;
	public Vector3 pusherGCP;

	GameObject pusherH;
	public Vector3 pusherHOP;
	public Vector3 pusherHNP;
	public Vector3 pusherHNP2;
	public Vector3 pusherHCP;

	GameObject pusherI;
	public Vector3 pusherIOP;
	public Vector3 pusherINP;
	public Vector3 pusherINP2;
	public Vector3 pusherICP;

	GameObject pusherJ;
	public Vector3 pusherJOP;
	public Vector3 pusherJNP;
	public Vector3 pusherJNP2;
	public Vector3 pusherJCP;

	GameObject pusherK;
	public Vector3 pusherKOP;
	public Vector3 pusherKNP;
	public Vector3 pusherKNP2;
	public Vector3 pusherKCP;

	GameObject pusherL;
	public Vector3 pusherLOP;
	public Vector3 pusherLNP;
	public Vector3 pusherLNP2;
	public Vector3 pusherLCP;

	GameObject pusherM;
	public Vector3 pusherMOP;
	public Vector3 pusherMNP;
	public Vector3 pusherMNP2;
	public Vector3 pusherMCP;

	GameObject pusherN;
	public Vector3 pusherNOP;
	public Vector3 pusherNNP;
	public Vector3 pusherNNP2;
	public Vector3 pusherNCP;

	//Lifts
	GameObject platFormOne;
	public Vector3 platFormOneOP;
	public Vector3 platFormOneNP;
	public Vector3 platFormOneNP2;
	public Vector3 platFormOneCP;

	GameObject platFormTwo;
	public Vector3 platFormTwoOP;
	public Vector3 platFormTwoNP;
	public Vector3 platFormTwoNP2;
	public Vector3 platFormTwoCP;

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

	public bool interact = true;

	public bool triggerLiftA = false;
	public bool triggerLiftB = false;
   

	// Use this for initialization
	void Start() {
		//Setting up original position and new position for bridges
		//========================================================================
		bridgeOne = GameObject.Find("Bridge1");
		bridgeOneOP = GameObject.Find("Bridge1").transform.position;
		bridgeOneNP = bridgeOne.transform.position + Vector3.down * 17f;

		bridgeTwo = GameObject.Find("Bridge2");
		bridgeTwoOP = GameObject.Find("Bridge2").transform.position;
		bridgeTwoNP = bridgeTwo.transform.position + -Vector3.right * 20f;

		bridgeThree = GameObject.Find("BridgeConnect1");
		bridgeThreeOP = GameObject.Find("BridgeConnect1").transform.position;
		bridgeThreeNP = bridgeThree.transform.position + -Vector3.right * 20f;
		//========================================================================

		//Setting up original position and new position for pushers
		//========================================================================
		pusher = GameObject.Find("Pusher");
		pusherOP = GameObject.Find("Pusher").transform.position;
		pusherNP = pusher.transform.position + -Vector3.forward * 9f;
		pusherNP2 = pusher.transform.position + Vector3.forward * 8f;

		pusherB = GameObject.Find("PusherB");
		pusherBOP = GameObject.Find("PusherB").transform.position;
		pusherBNP = pusherB.transform.position + Vector3.forward * 40f;

		pusherC = GameObject.Find("PusherC");
		pusherCOP = GameObject.Find("PusherC").transform.position;
		pusherCNP = pusherC.transform.position + Vector3.left * 11f;

		pusherD = GameObject.Find("PusherD");
		pusherDOP = GameObject.Find("PusherD").transform.position;
		pusherDNP = pusherD.transform.position + Vector3.left * 11f;

		pusherE = GameObject.Find("PusherE");
		pusherEOP = GameObject.Find("PusherE").transform.position;
		pusherENP = pusherE.transform.position + Vector3.forward * 11f;

		pusherF = GameObject.Find("PusherF");
		pusherFOP = GameObject.Find("PusherF").transform.position;
		pusherFNP = pusherF.transform.position + Vector3.forward * 11f;

		pusherG = GameObject.Find("PusherG");
		pusherGOP = GameObject.Find("PusherG").transform.position;
		pusherGNP = pusherG.transform.position + Vector3.forward * 11f;

		pusherH = GameObject.Find("PusherH");
		pusherHOP = GameObject.Find("PusherH").transform.position;
		pusherHNP = pusherH.transform.position + Vector3.right * 11f;

		pusherI = GameObject.Find("PusherI");
		pusherIOP = GameObject.Find("PusherI").transform.position;
		pusherINP = pusherI.transform.position + Vector3.right * 11f;

		pusherJ = GameObject.Find("PusherJ");
		pusherJOP = GameObject.Find("PusherJ").transform.position;
		pusherJNP = pusherJ.transform.position + Vector3.back * 11f;

		pusherK = GameObject.Find("PusherK");
		pusherKOP = GameObject.Find("PusherK").transform.position;
		pusherKNP = pusherK.transform.position + Vector3.back * 11f;

		pusherL = GameObject.Find("PusherL");
		pusherLOP = GameObject.Find("PusherL").transform.position;
		pusherLNP = pusherL.transform.position + Vector3.back * 11f;

		pusherM = GameObject.Find("PusherM");
		pusherMOP = GameObject.Find("PusherM").transform.position;
		pusherMNP = pusherM.transform.position + Vector3.back * 11f;

		pusherN = GameObject.Find("PusherN");
		pusherNOP = GameObject.Find("PusherN").transform.position;
		pusherNNP = pusherN.transform.position + Vector3.back * 11f;
		//========================================================================

		//Setting up original position and new position for lifts
		//========================================================================
		platFormOne = GameObject.Find("LifeSolutionA");
		platFormOneOP = GameObject.Find("LifeSolutionA").transform.position;
		platFormOneNP = platFormOne.transform.position + -Vector3.up * 26.8f;
		platFormOneNP2 = platFormOne.transform.position + Vector3.down * 26.8f;

		platFormTwo = GameObject.Find("LifeSolutionB");
		platFormTwoOP = GameObject.Find("LifeSolutionB").transform.position;
		platFormTwoNP = platFormTwo.transform.position + -Vector3.up * 36f;
		platFormOneNP2 = platFormTwo.transform.position + Vector3.down * 36f;
		//========================================================================
	}

	// Update is called once per frame
	void Update() {
		//Setting up current position for lifts
		//========================================================================
		bridgeOneCP = GameObject.Find("Bridge1").transform.position;
		bridgeTwoCP = GameObject.Find("Bridge2").transform.position;
		bridgeThreeCP = GameObject.Find("BridgeConnect1").transform.position;
		//========================================================================

		//Setting up current position for pushers
		//========================================================================
		pusherCP = GameObject.Find("Pusher").transform.position;
		pusherBCP = GameObject.Find("PusherB").transform.position;
		pusherCCP = GameObject.Find("PusherC").transform.position;
		pusherDCP = GameObject.Find("PusherD").transform.position;
		pusherECP = GameObject.Find("PusherE").transform.position;
		pusherFCP = GameObject.Find("PusherF").transform.position;
		pusherGCP = GameObject.Find("PusherG").transform.position;
		pusherHCP = GameObject.Find("PusherH").transform.position;
		pusherICP = GameObject.Find("PusherI").transform.position;
		pusherJCP = GameObject.Find("PusherJ").transform.position;
		pusherKCP = GameObject.Find("PusherK").transform.position;
		pusherLCP = GameObject.Find("PusherL").transform.position;
		pusherMCP = GameObject.Find("PusherM").transform.position;
		pusherNCP = GameObject.Find("PusherN").transform.position;
		//========================================================================

		//Setting up current position for lifts
		//========================================================================
		platFormOneCP = GameObject.Find("LifeSolutionA").transform.position;
		platFormTwoCP = GameObject.Find("LifeSolutionB").transform.position;
		//========================================================================

		moveBridge();
		movingPusher();
		movingPlatform();
		interaction();
	}

	void movingPusher() { 
		//PusherA
		//========================================================================
		if (changeA == false) {
			pusherCurrentLerpTime += Time.deltaTime;

			float Perc = pusherCurrentLerpTime / pusherLerpTime;

			if (pusherCurrentLerpTime >= pusherLerpTime) {
				changeA = true;
				pusherCurrentLerpTime = 0;
			}
			pusher.transform.position = Vector3.Lerp(pusherCP, pusherNP, Perc / 12);      
		}

		if (changeA == true) {
			pusherCurrentLerpTime += Time.deltaTime;

			float Perc = pusherCurrentLerpTime / pusherLerpTime;

			if (pusherCurrentLerpTime >= pusherLerpTime) {
				changeA = false;
				pusherCurrentLerpTime = 0;
			}
			pusher.transform.position = Vector3.Lerp(pusherCP, pusherOP, Perc / 10);
		}
		//========================================================================


		//PusherB
		//========================================================================
		if (changeB == false) {
			pusherBCurrentLerpTime += Time.deltaTime;

			float Perc = pusherBCurrentLerpTime / pusherBLerpTime;

			if (pusherBCurrentLerpTime >= pusherBLerpTime) {
				changeB = true;
				pusherBCurrentLerpTime = 0;
			}
			pusherB.transform.position = Vector3.Lerp(pusherBCP, pusherBNP, Perc / 50);
		}

		if (changeB == true) {
			pusherBCurrentLerpTime += Time.deltaTime;

			float Perc = pusherBCurrentLerpTime / pusherBLerpTime;

			if (pusherBCurrentLerpTime >= pusherBLerpTime) {
				changeB = false;
				pusherBCurrentLerpTime = 0;
			}
			pusherB.transform.position = Vector3.Lerp(pusherBCP, pusherBOP, Perc / 50);
		}
		//========================================================================


		//Pusher Sequence for 1 second
		//========================================================================
		if (changeSeqA == false) {
			SeqACurrentLerpTime += Time.deltaTime;

			float Perc = SeqACurrentLerpTime / seqALerpTime;

			if (SeqACurrentLerpTime >= seqALerpTime) {
				changeSeqA = true;
				SeqACurrentLerpTime = 0;
			}
			pusherC.transform.position = Vector3.Lerp(pusherCCP, pusherCNP, Perc / 50);
			pusherE.transform.position = Vector3.Lerp(pusherECP, pusherENP, Perc / 50);
			pusherI.transform.position = Vector3.Lerp(pusherICP, pusherINP, Perc / 50);
		}

		if (changeSeqA == true) {
			SeqACurrentLerpTime += Time.deltaTime;

			float Perc = SeqACurrentLerpTime / seqALerpTime;

			if (SeqACurrentLerpTime >= seqALerpTime) {
				changeSeqA = false;
				SeqACurrentLerpTime = 0;
			}
			pusherC.transform.position = Vector3.Lerp(pusherCCP, pusherCOP, Perc / 50);
			pusherE.transform.position = Vector3.Lerp(pusherECP, pusherEOP, Perc / 50);
			pusherI.transform.position = Vector3.Lerp(pusherICP, pusherIOP, Perc / 50);
		}
		//========================================================================

		//Pusher Sequence for 2 seconds
		//========================================================================
		if (changeSeqB == false) {
			SeqBCurrentLerpTime += Time.deltaTime;

			float Perc = SeqBCurrentLerpTime / seqBLerpTime;

			if (SeqBCurrentLerpTime >= seqBLerpTime) {
				changeSeqB = true;
				SeqBCurrentLerpTime = 0;
			}
			pusherD.transform.position = Vector3.Lerp(pusherDCP, pusherDNP, Perc / 50);
			pusherG.transform.position = Vector3.Lerp(pusherGCP, pusherGNP, Perc / 50);
			pusherH.transform.position = Vector3.Lerp(pusherHCP, pusherHNP, Perc / 50);

		}

		if (changeSeqB == true) {
			SeqBCurrentLerpTime += Time.deltaTime;

			float Perc = SeqBCurrentLerpTime / seqBLerpTime;

			if (SeqBCurrentLerpTime >= seqBLerpTime) {
				changeSeqB = false;
				SeqBCurrentLerpTime = 0;
			}
			pusherD.transform.position = Vector3.Lerp(pusherDCP, pusherDOP, Perc / 50);
			pusherG.transform.position = Vector3.Lerp(pusherGCP, pusherGOP, Perc / 50);
			pusherH.transform.position = Vector3.Lerp(pusherHCP, pusherHOP, Perc / 50);
		}
		//========================================================================

		//Pusher Sequence for 3 seconds
		//========================================================================
		if (changeSeqC == false) {
			SeqCCurrentLerpTime += Time.deltaTime;

			float Perc = SeqCCurrentLerpTime / seqCLerpTime;

			if (SeqCCurrentLerpTime >= seqCLerpTime) {
				changeSeqC = true;
				SeqCCurrentLerpTime = 0;
			}
			pusherF.transform.position = Vector3.Lerp(pusherFCP, pusherFNP, Perc / 50);
            

		}

		if (changeSeqC == true) {
			SeqCCurrentLerpTime += Time.deltaTime;

			float Perc = SeqCCurrentLerpTime / seqCLerpTime;

			if (SeqCCurrentLerpTime >= seqCLerpTime) {
				changeSeqC = false;
				SeqCCurrentLerpTime = 0;
			}
			pusherF.transform.position = Vector3.Lerp(pusherFCP, pusherFOP, Perc / 50);
          
		}
		//========================================================================


		//Pusher sequence
		//========================================================================
		if (changeSeqD == false) {
			SeqDCurrentLerpTime += Time.deltaTime;

			float Perc = SeqDCurrentLerpTime / seqDLerpTime;

			if (SeqDCurrentLerpTime >= seqDLerpTime) {
				changeSeqD = true;
				SeqDCurrentLerpTime = 0;
			}
			if (SeqDCurrentLerpTime >= 1 && SeqDCurrentLerpTime <= 2) {
				pusherJ.transform.position = Vector3.Lerp(pusherJCP, pusherJNP, Perc / 50);
			} else if (SeqDCurrentLerpTime >= 2 && SeqDCurrentLerpTime <= 3) {
				pusherK.transform.position = Vector3.Lerp(pusherKCP, pusherKNP, Perc / 50);
			} else if (SeqDCurrentLerpTime >= 3 && SeqDCurrentLerpTime <= 4) {
				pusherM.transform.position = Vector3.Lerp(pusherMCP, pusherMNP, Perc / 50);
			} else if (SeqDCurrentLerpTime >= 4 && SeqDCurrentLerpTime <= 5) {
				pusherL.transform.position = Vector3.Lerp(pusherLCP, pusherLNP, Perc / 50);
				pusherN.transform.position = Vector3.Lerp(pusherNCP, pusherNNP, Perc / 50);
			} else if (SeqDCurrentLerpTime >= 5 && SeqDCurrentLerpTime <= 6) {
				pusherM.transform.position = Vector3.Lerp(pusherMCP, pusherMOP, Perc / 50);
			} else if (SeqDCurrentLerpTime >= 5 && SeqDCurrentLerpTime <= 7) {
				pusherN.transform.position = Vector3.Lerp(pusherNCP, pusherNOP, Perc / 50);
			}
		}

		if (changeSeqD == true) {
			SeqDCurrentLerpTime += Time.deltaTime;

			float Perc = SeqDCurrentLerpTime / seqCLerpTime;

			if (SeqDCurrentLerpTime >= seqCLerpTime) {
				changeSeqD = false;
				SeqDCurrentLerpTime = 0;
			}
			pusherJ.transform.position = Vector3.Lerp(pusherJCP, pusherJOP, Perc / 50);
			pusherK.transform.position = Vector3.Lerp(pusherKCP, pusherKOP, Perc / 50);
			pusherL.transform.position = Vector3.Lerp(pusherLCP, pusherLOP, Perc / 50);
			pusherM.transform.position = Vector3.Lerp(pusherMCP, pusherMOP, Perc / 50);
			pusherN.transform.position = Vector3.Lerp(pusherNCP, pusherNOP, Perc / 50);
		}
		//========================================================================
	}

	//Moving lifts with lerp time
	void movingPlatform() {
		if (triggerLiftA) {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime >= lerpTime) {
				currentLerpTime = lerpTime;
			}
			float Perc = currentLerpTime / lerpTime;
			platFormOne.transform.position = Vector3.Lerp(platFormOneCP, platFormOneNP, Perc / 30);
		}

		if (triggerLiftB) {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime >= lerpTime) {
				currentLerpTime = lerpTime;
			}
			float Perc = currentLerpTime / lerpTime;
			platFormTwo.transform.position = Vector3.Lerp(platFormTwoCP, platFormTwoNP, Perc / 30);
		}
	}

	//Moving the bridges with lerp time
	void moveBridge() {
		if (triggerBridge) {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime >= lerpTime) {
				currentLerpTime = lerpTime;
			}
			float Perc = currentLerpTime / lerpTime;
			bridgeOne.transform.position = Vector3.Lerp(bridgeOneCP, bridgeOneNP, Perc / 10);
		}

		if (triggerB) {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime >= lerpTime) {
				currentLerpTime = lerpTime;
			}
			float Perc = currentLerpTime / lerpTime;
			bridgeTwo.transform.position = Vector3.Lerp(bridgeTwoCP, bridgeTwoNP, Perc / 10);
		}

		if (triggerA) {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime >= lerpTime) {
				currentLerpTime = lerpTime;
			}
			float Perc = currentLerpTime / lerpTime;
			bridgeThree.transform.position = Vector3.Lerp(bridgeThreeCP, bridgeThreeNP, Perc / 10);
		}
	}

	void interaction() {
		if (Input.GetKey("f") || Input.GetKey("joystick button 2")) {
			interact = true;
		} else {
			interact = false;
		}

	}

	//When the player enters the trigger, it will activate the boolean
	void OnTriggerStay(Collider Col) {
		//Player activating first bridge
		if ((Col.tag == "Player" && gameObject.tag == ("ButtonBridge")) && interact) {
			triggerBridge = true;
		}

		//Player activat
		if ((Col.tag == "Player" && gameObject.tag == ("ButtonABridge")) && interact) {
			triggerA = true;
		}

		if ((Col.tag == "Player" && gameObject.tag == ("ButtonBBridge")) && interact) {
			triggerB = true;
		}

		if ((Col.tag == "Player" && gameObject.tag == ("TriggerLiftA")) && interact) {
			triggerLiftA = true;
		}

		if ((Col.tag == "Player" && gameObject.tag == ("TriggerLiftB")) && interact) {
			triggerLiftB = true;
		}
	}

	//When the player exits triggers, it will reset every values to default
	void OnTriggerExit(Collider Col) {
		triggerBridge = false;
		triggerA = false;
		triggerB = false;
		triggerLiftA = false;
		triggerLiftB = false;

		if (currentLerpTime == lerpTime) {
			currentLerpTime = 0;
		} else {
			currentLerpTime = 0;
		}
	}
}