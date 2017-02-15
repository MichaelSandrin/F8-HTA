using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {


    Vector3 respawnPoint;

    Vector3 respawnPoint2;

    //Character Controller
    public Vector3 characterPosition;
    private CharacterController character;

    Vector3 cameraMovement;

    Transform respawnLevel1;
    Transform death;

    Transform respawnLevel2;


    public Material[] material;
    public Renderer rend;

    //Movement Variables
    public float HAxis;
    public float VAxis;
    //public float[] characterPosition;
    public float forwardVelocity;
    public float backwardVelocity;
    public float rightVelocity;
    public float leftVelocity;
    public float fowardAcceleration;
    public float sideAcceleration;
    public float acceleration;
    public float deceleration;
    public float sideVelocity;
    public float maxSideVelocity;
    public float maxBackwardVelocity;
    public float maxVelocity;
    public float rotateVelocity;
    Quaternion characterRotation;


    public float distToGrounded = 0.1f;
    public LayerMask ground;

    public Vector3 velocity;

    public float verticalVelocity;
    public float playerGravity;

    //Jump Variables
    public bool onGround;
    public float groundSphere = 0.3f;
    //public LayerMask theGround;
    public float jumpVelocity;
    public float jumpForce;
    public float jumpAcceleration;
    public float maxHeight;
    public float fallVelocity;

    //Glide Variables
    public float ambientSpeed = 100.0f;
    public float rotationSpeed = 200.0f;

    public float glideLockoutTimer = 50f;
    public float wingEdurance = 0;
    public float maxWingEndurance = 50;
    public float dragCoefficient = 100f;
    public float dragForce = 5f;

    public float deathVelocity;

    //Climbing Ladder
    public bool inside;
    public bool interact;
    public float heightFactor = 7f;
    public Transform ChController;

    //Climb Edge
    public bool canClimb;
    public Animator anim;

    /*
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
    */
  

    float LStickX;
    float LStickY;


    public Quaternion CharacterRotation
    {
        get
        {
            return characterRotation;
        }
    }

	// Use this for initialization
	void Start () {

        //Character initialization
        character = GetComponent<CharacterController>();
        rend = GetComponent<Renderer>();
        rend.enabled = true;

        //material[0] = new Color(255, 108, 106, 1);
        //rend.sharedMaterial = material[0];

        respawnPoint = GameObject.Find("SpawnPoint").transform.position;
        //respawnPoint = new Vector3(7.17f, 4.37f, 7.075f);
        //respawnPoint2 = new Vector3(4.0f, 0.4f,-23f);

        characterRotation = transform.rotation;
     
        forwardVelocity = 0f;
        backwardVelocity = 0f;
        rightVelocity = 0f;
        leftVelocity = 0f;

        sideAcceleration = 1f;
        acceleration = 3f;
        fowardAcceleration = 3f;
        deceleration = .5f;
        rotateVelocity = 200f;
        maxVelocity = 4f;
        maxSideVelocity = 3f;
        maxBackwardVelocity = 2f;


        verticalVelocity = 0f;
        playerGravity = -9.80f;

        jumpAcceleration = 1f;
        jumpVelocity = 0f;
        jumpForce = 6f;
        maxHeight = 5f;

        inside = false;
        interact = false;

        /*
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
        */

    //Physics.gravity = new Vector3(0, -100F, 0);
}
	
	// Update is called once per frame
	void Update () {
        HAxis = Input.GetAxis("Horizontal");
        VAxis = Input.GetAxis("Vertical");
        /*
        hangerOneCP = GameObject.Find("hangingPlatform1").transform.position;
        hangerTwoCP = GameObject.Find("hangingPlatform2").transform.position;
        hangerThreeCP = GameObject.Find("hangingPlatform3").transform.position;
        hangerFourCP = GameObject.Find("hangingPlatform4").transform.position;
        hangerFiveCP = GameObject.Find("hangingPlatform5").transform.position;
        hangerSixCP = GameObject.Find("hangingPlatform6").transform.position;
        hangerSevenCP = GameObject.Find("hangingPlatform7").transform.position;
        */



        LStickX = Input.GetAxis("X360_LStickX");
        LStickY = Input.GetAxis("X360_LStickY");

        
        /*
        print(HAxis);
        print(VAxis);
        print("X" +LStickX);
        print("Y" + -LStickY);*/
        walk();
        
        climbLadder();
        //moveHangerPlateForm();
        fallVelocity = character.velocity.y;
    }

    void FixedUpdate()
    {
        turn();
        run();
    }

    void walk()
    {

       // print(GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color);
        //Up arrow and W with acceleration
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
           
            forwardVelocity += (fowardAcceleration);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255/255, 108/255, 106/255, 255/255);
            //character.GetComponent<Renderer>().material.color = new Color(255, 108, 106, 1);
        }else if (-Input.GetAxis("X360_LStickY") > 0)
        {
            forwardVelocity += -(LStickY) * fowardAcceleration;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        } 
        //Down arrow and s with acceleration
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            backwardVelocity += acceleration;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        }
        else if (-Input.GetAxis("X360_LStickY") < 0)
        {
            backwardVelocity += (LStickY);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        }

        //Right arrow and D with acceleration
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rightVelocity += sideAcceleration;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        }
        else if (Input.GetAxis("X360_LStickX") > 0)
        {
            rightVelocity += (LStickX) * sideAcceleration;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        }

        //Left arrow and A with acceleration
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            leftVelocity += sideAcceleration;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        }
        else if (Input.GetAxis("X360_LStickX") < 0)
        {
            leftVelocity += -(LStickX) * sideAcceleration;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        }

        //Deceleration for forward, backward, right, and left velocity
        forwardVelocity -= deceleration;
        forwardVelocity = Mathf.Clamp(forwardVelocity, 0, maxVelocity);
        backwardVelocity -= deceleration;
        backwardVelocity = Mathf.Clamp(backwardVelocity, 0, maxBackwardVelocity);
        rightVelocity -= deceleration;
        rightVelocity = Mathf.Clamp(rightVelocity, 0, maxSideVelocity);
        leftVelocity -= deceleration;
        leftVelocity = Mathf.Clamp(leftVelocity, 0, maxSideVelocity);

        verticalVelocity = character.velocity.y;
        
        verticalVelocity += playerGravity * Time.deltaTime;
        jump();
        glide();
        //Apply the directional movement to the character


        cameraMovement = Camera.main.transform.forward;
        cameraMovement.y = 0;
       
        character.Move(((cameraMovement * forwardVelocity) + (-cameraMovement * backwardVelocity) + (Camera.main.transform.right * rightVelocity) + (-Camera.main.transform.right * leftVelocity)) * Time.deltaTime);
        //character.Move(((Vector3.forward * forwardVelocity) + (Vector3.back * backwardVelocity) + (Vector3.right * rightVelocity) + (Vector3.left * leftVelocity)) *Time.deltaTime);
        //character.Move(((Vector3.forward * -VAxis * 3) + (Vector3.back * -VAxis * 3) + (Vector3.right * HAxis * 3) + (Vector3.left * HAxis * 3)) * Time.deltaTime);
        character.Move((Vector3.up * verticalVelocity) * Time.deltaTime);
       
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateVelocity, 0);
        }
        else if (Input.GetKey(KeyCode.E))
        {

            transform.Rotate(0, Input.GetAxis("Horizontal") * -rotateVelocity, 0);
        }

        


    }

    void run()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            forwardVelocity = 15;
        }
    }

    void turn()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            characterRotation *= Quaternion.Euler(0,rotateVelocity * -1 * Time.deltaTime, 0);
            transform.rotation = characterRotation;
        }else if (Input.GetKey(KeyCode.E))
        {
            characterRotation *= Quaternion.AngleAxis(rotateVelocity * 1 * Time.deltaTime, Vector3.up);
            transform.rotation = characterRotation;
        }
        /*
        Vector3 NextDir = new Vector3(Input.GetAxis("X360_RStickX"), 0, Input.GetAxis("X360_RStickY"));
        if (NextDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(NextDir);
        }*/
    }

    private float calculateDragForce()
    {
        return (character.velocity.y * character.velocity.y * dragCoefficient) / 2;
    }

    void glide()
    {
        //Reset wingEndurance when landing


        if (character.isGrounded)
        {
            wingEdurance = maxWingEndurance;
            glideLockoutTimer = 20f;
            
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
        }

        if(character.isGrounded && (Input.GetButton("Jump") || Input.GetButtonDown("X360_A")))
        {
            verticalVelocity += 1f * Time.deltaTime;
            
            //character.velocity.y = verticalVelocity;
            // character.Move();
        }

        if (!character.isGrounded && verticalVelocity < 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(0.227f, 0.227f, 0.227f, 1);
            if (Input.GetButton("Jump") && glideLockoutTimer !=0  )
            {
                //renderer.material.color = new Color(0.5f, 1, 1);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(0.1059f, 0.9137f, 0.988f, 1);
                verticalVelocity += dragForce * Time.deltaTime;
                //verticalVelocity -= playerGravity * Time.deltaTime;
                glideLockoutTimer -= .25f;
                
                //wingEdurance -= Time.deltaTime;

            }

            
           
        }

        if(fallVelocity < -9 && inside != true)
        {

            character.transform.position = respawnPoint;
            print("Death");
        }

        


    }

    
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((character.collisionFlags & CollisionFlags.Sides) != 0)
        {
            //print("touched the side");
            //verticalVelocity = 0;
            
        }

        //print((character.collisionFlags & CollisionFlags.Below) != 0);
        if(hit.normal.y != 1 && hit.controller.detectCollisions)
        {
            glideLockoutTimer = 0;
            
        }

        if (!(character.collisionFlags == CollisionFlags.Below) && hit.controller.detectCollisions)
        {
           // print("fly");
        }
        //print(hit.controller.detectCollisions);
        //print(hit.normal.y);
        //print(character.isGrounded);
        //print(character.collisionFlags);
        //Debug.Log(hit.moveDirection);
    
    }
    
  

    void jump()
    {
        if (Input.GetButtonDown("Jump") && character.isGrounded)
        {
           
            verticalVelocity += 4f;
            //character.renderer.material.color = new Color(27, 233, 252, 1);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(27, 233, 252, 1);
        }
    }
    
    void climbLadder()
    {
        if(inside == true && (Input.GetKey("w") || -Input.GetAxis("X360_LStickY") > 0))
        {
            ChController.transform.position += Vector3.up / heightFactor;
        }else if (inside == true && (Input.GetKey("s") || -Input.GetAxis("X360_LStickY") < 0))
        {
            ChController.transform.position += Vector3.down / heightFactor;
        }
        



    }

    void interaction()
    {
        if (Input.GetKey("f") || Input.GetKey("joystick button 2"))
        {
            interact = !interact;
        }
        else
        {
            //interact = false;
        }
    }

    /*
    void climbEdge()
    {
        if (canClimb && Input.GetKeyDown(KeyCode.R))
        {
            character.enabled = false;

            anim.SetTrigger("Climb");
            StartCoroutine(afterClimb());
        }
    }

    IEnumerator afterClimb()
    {
        yield return new WaitForSeconds(1);
        character.enabled = true;
        transform.position = character.transform.position;
 
    }*/

    /*
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
            hangerOne.transform.position = Vector3.Lerp( hangerOneCP, hangerOneOP, Perc);
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

    }*/
    

    void OnTriggerEnter(Collider Col)
    {
        //Debug.Log("Entered Trigger");
        interaction();
        if (Col.gameObject.tag == "Ladder")
        {
            
            //currentLerpTime += Time.deltaTime;
            character.enabled = false;
            inside = !inside;
            characterPosition = character.transform.position;
        }

        /*
        if(Col.gameObject.tag == "ButtonReset")
        {
            triggerReset = true;
            
            print("Reset Pressed");
        }
        if (Col.gameObject.tag == "ButtonOne")
        {
            
            triggerButtonOne = true;
        }
        if (Col.gameObject.tag == "ButtonTwo")
        {
            triggerButtonTwo = true;
        }
        if (Col.gameObject.tag == "ButtonThree")
        {
            triggerButtonThree = true;
        }
        */
        if (Col.gameObject.tag == "Climb")
        {
            canClimb = true;
        }

        if (Col.gameObject.tag == "Exit")
        {
            //character.transform.position = respawnPoint2;
            print("Yo");
            Application.LoadLevel("LadderPuzzle");
        }

        if (Col.gameObject.tag == "ExitLevel2")
        {
            character.transform.position = respawnPoint;
        }



    }


    
    void OnTriggerExit(Collider Col)
    {
        //triggerReset = false;
        //triggerButtonOne = false;
        //triggerButtonTwo = false;
        //triggerButtonThree = false;
        if (Col.gameObject.tag == "Ladder")
        {
            character.enabled = true;
            inside = !inside;

        }
        /*
        if (currentLerpTime == lerpTime)
        {
            currentLerpTime = 0;
        }else
        {
            currentLerpTime = 0;
        }*/
        canClimb = false;
    }
    

    /*
    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGrounded, ground);
    }
    */
    
    void OnCollisionEnter(Collision collisionInfo)
    {
        onGround = true;
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        onGround = false;
    }
    

}
