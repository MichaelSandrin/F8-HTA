using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	// ***Important***
	// Variables initialized outside of Start() will we editable through the editor but will not reset when Unity is restarted.
	// To return variables to their initialized values stated inside the script, in the editor hit the gear icon and select 'reset'.

	// To Do:
	// Currently horizontal velocities aren't being checked against the player (will show full falue even when running into wall)
	// on the other hand vertical velocity is being checked against the player (will zero out when grounded)
	// checking vertical velocity is dangerous because if the player jumps up a step they will be launched into the air
	// Glide really needs to be based on vertical velocity or else it barely works at high speeds
	// Character always returns to face forward
	// Make deceleration faster than acceleration
	// Motion sometimes locks out if swapping direction keys exactly as the other is realeased


	// Game
	public Vector3 respawnPoint;

	// Player
	private CharacterController player;

	// Camera
	public Camera playerCamera;
	// Needs "Main Camera" Attached
	public Transform ChController;
	// Needs "Plume" (character transfrom) attached to this

	Vector3 cameraDirection;

	// Input
	Vector3 keyboardLateralInput = new Vector3 (0, 0, 0);
	Vector3 controllerLateralInput = new Vector3 (0, 0, 0);
	Vector3 combinedLateralInput = new Vector3 (0, 0, 0);

	private float fbVelocity = 0f;
	private float lrVelocity = 0f;
	Vector3 relativeVelocity = new Vector3 (0, 0, 0);

	private float forwardVelocity = 0f;
	private float backwardVelocity = 0f;
	private float rightVelocity = 0f;
	private float leftVelocity = 0f;

	private float acceleration = 2f;
	private float deceleration = 1f;

	public float maxSpeedOriginal = 8f;
	public float maxSpeed = 0f;
	private float maxSpeedOuter = 0f;
	private float maxSpeedInner = 0f;

	// Vertical Movement
	public float jumpSpeed = 6f;

	// Jump
	public bool onGround;
	public float groundSphere = 0.3f;

	public float distToGrounded = 0.1f;
	public LayerMask ground;

	public float verticalVelocity = 0f;
	public float playerGravity = -9.8f;

	// Glide
	public float glideLockoutTimer = 5f;
	public float maxGlideSpeed = 3f;
	public float glideSpeed = 0f;

	// This is really finicky and needs to be within 8-9 to be noticeable.
	private float dragForce = 8.8f;
	private float dragForce2 = 0f;


	// Climb
	public bool inside = false;
	public bool interact = false;
	public float climbSpeed = 7f;

	// Push
	public float pushPower = 5.0f;

	// Rendering
	public Material[] material;
	// What is this?
	public Renderer rend;

	public float characterRotateSpeed = 20f;

	// Redundant Code
	/*
	public float HAxis;
	public float VAxis;

    public Quaternion CharacterRotation {
        get {
            return characterRotation;
        }
    }
   	*/

	void Start ()
	{
		// Game
		respawnPoint = GameObject.Find ("SpawnPoint").transform.position;

		// Character
		player = GetComponent<CharacterController> ();
		rend = GetComponent<Renderer> ();
		rend.enabled = true;

		//material[0] = new Color(255, 108, 106, 1);
		//rend.sharedMaterial = material[0];
		//Physics.gravity = new Vector3(0, -100F, 0);
	}

	void Update ()
	{
		CalculateCameraDirection ();
		PlayerInput ();
		RenderModel ();

		// Redundant Code
		/*
        HAxis = Input.GetAxis("Horizontal");
        VAxis = Input.GetAxis("Vertical");
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - previousPosition), Time.deltaTime * characterRotateSpeed);
		moveHangerPlateForm();
		*/
	}

	void CalculateCameraDirection ()
	{
		cameraDirection = playerCamera.transform.forward; // Takes the z axis from the camera.transform.
		cameraDirection.y = 0; // Ignores the y component.
		cameraDirection.Normalize (); // Creates a unit vector which wont be a super small value when the camera is looking down.
	}

	void PlayerInput ()
	{
		// Get Discrete Circular Input for Lateral Acceleration
		// Convert square vector to circular vector.
		keyboardLateralInput = Vector3.Normalize (new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"))); // Keyboard axis are (right, up) positive, Square // We have our own acceleration and dont need keyboard input acceleration

		// Convert square vector to circular vector.
		controllerLateralInput = Vector3.Normalize (new Vector3 (Input.GetAxis ("X360_LStickX"), 0, -Input.GetAxis ("X360_LStickY"))); // Joysticks are (right, down) positive, Square

		// Get Absolute Joystick Input Magnitude for maxSpeed multiplication.
		// Convert the discrete circular vector to a continuous circular vector.
		controllerLateralInput.x = controllerLateralInput.x * Mathf.Abs (Input.GetAxis ("X360_LStickX")); // This is applying the ratio from 0 to 1 for fractional joystick inputs because normalize forces all vectors to be either 0 or 1 units long.
		controllerLateralInput.z = controllerLateralInput.z * Mathf.Abs (Input.GetAxis ("X360_LStickY"));

		// Combine both keyboard and controller inputs (keyboard effectively overrides the controller for same directions (because it's discrete) but still feels influence in opposite directions).
		combinedLateralInput = keyboardLateralInput + controllerLateralInput;
		// Clamps the new vector length to 1 (max input length)
		combinedLateralInput = Vector3.ClampMagnitude (combinedLateralInput, 1); // Clamps the vector length to 1 (don't need to worry about square vectors because both are already circular, this wont work for the controller in place of the normalize process because it wont properly convert the square ratios to circular ratios).  
	}

	void RenderModel ()
	{
		// Turn the character with smoothing. Lerp(fromLocation, toLocation, time * turnSpeed);
		//transform.rotation = Quaternion.Lerp(transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w), Quaternion.LookRotation(player.velocity), Time.deltaTime * characterRotateSpeed);
	}

	// Called once per physics calculation (fixed timeline). Is called just before physics (rigidbody) are updated. Should be used for physics calculations.
	void FixedUpdate ()
	{
		// Movement
		Walk ();
		Jump ();
		Glide2 ();

		Climb ();
		Interaction ();

		ApplyMotion (); // Must come after all movement updates.

		// Redundant Code
		/*
		 turn();
		*/
	}

	float shortestAngleBetween (Vector3 fromVector, Vector3 toVector)
	{
		float angle = Mathf.Atan2 (fromVector.z, fromVector.x) - Mathf.Atan2 (toVector.z, toVector.x);

		float tempX = Mathf.Cos (angle);
		float tempY = Mathf.Sin (angle);

		angle = Mathf.Atan2 (tempY, tempX);

		return (angle); // Returns an angle in radians
	}

	void ApplyMotion ()
	{
		// Apply Motion // A single call to Move() must be made for velocity to properly work (ie not just show the last spliced velocity).
		player.Move (((cameraDirection * relativeVelocity.z) + (playerCamera.transform.right * relativeVelocity.x) + (Vector3.up * verticalVelocity)) * Time.deltaTime);

		Vector3 temp = new Vector3 (player.velocity.x, 0, player.velocity.z);
		//print(temp.magnitude);
	}

	Vector3 ChangeMagnitude (Vector3 direction, float magnitude)
	{
		Vector3 temp = new Vector3 (0, 0, 0);
		if (direction.magnitude > 0) {
			temp = (magnitude / direction.magnitude) * direction;
		}
		return temp;
	}

	void Walk ()
	{
		// Get the player velocity and convert it to a velocity relative to the camera.

		relativeVelocity = player.velocity;
		relativeVelocity.y = 0;
		relativeVelocity = Quaternion.Euler (0, shortestAngleBetween (cameraDirection, Vector3.forward) * Mathf.Rad2Deg, 0) * relativeVelocity; // Quaternions must be on the left side of a '*' operation for calculus reasons.

		// Apply Deceleration
		if (relativeVelocity.z > -deceleration && relativeVelocity.z < deceleration) { // Cut out any values lower than the deceleration rate to remove oscillation and allow the player to stand still.
			relativeVelocity.z = 0;
		}
		if (relativeVelocity.z >= deceleration) {
			relativeVelocity.z -= deceleration;
		}
		if (relativeVelocity.z <= -deceleration) {
			relativeVelocity.z += deceleration;
		}

		if (relativeVelocity.x > -deceleration && relativeVelocity.x < deceleration) {
			relativeVelocity.x = 0;
		}
		if (relativeVelocity.x >= deceleration) {
			relativeVelocity.x -= deceleration;
		}
		if (relativeVelocity.x <= -deceleration) {
			relativeVelocity.x += deceleration;
		}

		// Joystick magnitude controls maxSpeed not acceleration rate.
		maxSpeedOuter = maxSpeedOriginal * combinedLateralInput.magnitude; // Actual maxSpeed multiplied by the joystick magnitude.
		maxSpeedInner = (maxSpeedOriginal - acceleration) * combinedLateralInput.magnitude; // maxSpeed minus acceleration (for determining if the next speed will be greater or less than the actual maxSpeed.
		if (maxSpeedInner < 0) { // Clamp above 0.
			maxSpeedInner = 0;
		}

		// Apply Acceleration
		if (relativeVelocity.magnitude > maxSpeedInner) { // If the new speed has any possibility of being over the maxSpeed:
			Vector3 excess = relativeVelocity - ChangeMagnitude (relativeVelocity, maxSpeedOuter); // Calculate any pre-existing velocity over the maxSpeed.
			Vector3 temp = ChangeMagnitude (relativeVelocity, maxSpeedOuter) + excess; // Save it.
			if (temp.magnitude < maxSpeedOuter) { // If it is negative speed (derived from speeds above maxSpeedInner but below maxSpeedOuter, negate it.
				excess = new Vector3 (0, 0, 0);
			}

			relativeVelocity -= excess; // Remove the excess velocity the current velocity.

			relativeVelocity += new Vector3 (acceleration * combinedLateralInput.x, 0, acceleration * combinedLateralInput.z); // Add new acceleration.
			relativeVelocity = ChangeMagnitude (relativeVelocity, maxSpeedOuter); // Chop off excess acceleration but maintain the new direction.

			relativeVelocity += excess; // Add back the excess.

			// Note: What this basically does is allowing the player to change their input direction (and therefore input acceleration) when traveling at above the maximum walking speed.
			// If the excess removal and readdition was not in place and the player was traveling at above maximum walking speed, any excess speed generated from pushers or fans would be immediately chopped off.
			// If the input acceleration was simply based off of the combined input and excess velocities, then the excess velocity would heavily influence the input acceleration and it would be very difficult to change direction.
		} else {
			relativeVelocity += new Vector3 (acceleration * combinedLateralInput.x, 0, acceleration * combinedLateralInput.z); // If not, simply add the input acceleration.
		}

		// Redundant Code
		// Velocity Clamp // Not actually needed, we want to maintain momentum from pushers too
		//fbVelocity = Mathf.Clamp(fbVelocity, -maxSpeed, maxSpeed);
		//lrVelocity = Mathf.Clamp(lrVelocity, -maxSpeed, maxSpeed);

		/*
		//This goes at the start of Walk()
		forwardVelocity -= deceleration;
		backwardVelocity -= deceleration;
		leftVelocity -= deceleration;
		rightVelocity -= deceleration;
		*/

		/*
		// Forward
		if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
			forwardVelocity += acceleration;
		} else if (Input.GetAxis("X360_LStickY") < 0) { // Keyboard overrides controller.
			forwardVelocity -= Input.GetAxis("X360_LStickY") * acceleration;
		}

		// Backward
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
			backwardVelocity += acceleration;
		} else if (Input.GetAxis("X360_LStickY") > 0) {
			backwardVelocity += Input.GetAxis("X360_LStickY");
		}

		// Left
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			leftVelocity += acceleration;
		} else if (Input.GetAxis("X360_LStickX") < 0) {
			leftVelocity -= Input.GetAxis("X360_LStickX") * acceleration;
		}

		// Right
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			rightVelocity += acceleration;
		} else if (Input.GetAxis("X360_LStickX") > 0) {
			rightVelocity += Input.GetAxis("X360_LStickX") * acceleration;
		}
		*/

		/* // Clamp
		forwardVelocity = Mathf.Clamp(forwardVelocity, 0, maxSpeed);
		backwardVelocity = Mathf.Clamp(backwardVelocity, 0, maxSpeed);
		leftVelocity = Mathf.Clamp(leftVelocity, 0, maxSpeed);
		rightVelocity = Mathf.Clamp(rightVelocity, 0, maxSpeed);
		*/

		/*
		Camera.main.transform.position = character.transform.position;
		character.Move(((cameraDirection * forwardVelocity) + (-cameraDirection * backwardVelocity) + (character.transform.right * rightVelocity) + (-character.transform.right * leftVelocity)) * Time.deltaTime);
		character.Move(((Vector3.forward * forwardVelocity) + (Vector3.back * backwardVelocity) + (Vector3.right * rightVelocity) + (Vector3.left * leftVelocity)) *Time.deltaTime);
		character.Move(((Vector3.forward * -VAxis * 3) + (Vector3.back * -VAxis * 3) + (Vector3.right * HAxis * 3) + (Vector3.left * HAxis * 3)) * Time.deltaTime);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotateVelocity * -1 * Time.deltaTime, 0), Time.deltaTime * characterRotateSpeed);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - previousPosition, Vector3.up), Time.deltaTime * characterRotateSpeed);
		*/
	}

	void Jump ()
	{ // Do I reset horizontal velocities or do I free vertical velocity?
		if (player.isGrounded) {
			verticalVelocity = 0;
		}
		//verticalVelocity = player.velocity.y;
		verticalVelocity += playerGravity * Time.deltaTime; // Why is time used here? Works, don't know why. is it because acceleration is ms/^2? Then why not in movement?

		if (Input.GetButton ("Jump") && player.isGrounded) { // GetButtonDown is called once per button push.
			verticalVelocity += jumpSpeed;
			// Redundant Code
			//character.renderer.material.color = new Color(27, 233, 252, 1);
			//GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
			//GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(27, 233, 252, 1);
		}
		// Redundant Code
		//character.transform.rotation = Quaternion.Lerp(character.transform.rotation, Quaternion.LookRotation(character.transform.position - previousPosition), Time.deltaTime * characterRotateSpeed);
	}

	// drag = coefficient * velocity^2 / 2

	void Glide2 ()
	{
		if (player.isGrounded) {
			glideLockoutTimer = 5f;
		}



		if (!player.isGrounded && verticalVelocity < -jumpSpeed) {
			float coefficient = 2f;
			dragForce2 = coefficient * player.velocity.y * player.velocity.y / 2;

			print (dragForce2);

			if (Input.GetButton ("Jump") && glideLockoutTimer > 0) {

				verticalVelocity += dragForce2 * Time.deltaTime;

				glideLockoutTimer -= 1f * Time.deltaTime;
			}
		}
	}

	void Glide ()
	{
		// Glide Reset if Grounded
		if (player.isGrounded) {
			glideLockoutTimer = 5f;
			//glideSpeed = 0f;

			//GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(255 / 255, 108 / 255, 106 / 255, 255 / 255);
		}

		if (!player.isGrounded && verticalVelocity < -jumpSpeed) { // Is falling? // Ensures that players can't glide until they start falling.

			if (Input.GetButton ("Jump") && glideLockoutTimer > 0) { // Is Gliding?
				// Apply Glider Drag Force
				verticalVelocity += dragForce * Time.deltaTime;

				// Reduce Glide Timer
				glideLockoutTimer -= 1f * Time.deltaTime;

				//glideSpeed -= deceleration;
				//glideSpeed = Mathf.Clamp(glideSpeed, 0, maxGlideSpeed);
				//glideSpeed += (1f);

				//verticalVelocity -= playerGravity * Time.deltaTime;
				//renderer.material.color = new Color(0.5f, 1, 1);
				//GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(0.1059f, 0.9137f, 0.988f, 1);
				//wingEdurance -= Time.deltaTime;
			}
			//GameObject.FindGameObjectWithTag("Player").GetComponent<Renderer>().material.color = new Color(0.227f, 0.227f, 0.227f, 1);

			// Redundant Code
			//flightControll();
			//character.enabled = true;
		}

		/*
        if(fallVelocity < -18 && inside != true)
        {

            character.transform.position = respawnPoint;
            print("Death");
        }
        */
	}

	void push ()
	{

	}

   

	void OnControllerColliderHit (ControllerColliderHit hit)
	{

		Rigidbody body = hit.collider.attachedRigidbody;
		// Sets the glide timer to 0 if the player hits a wall.
		if (hit.normal.y != 1 && hit.controller.detectCollisions) { // 'wall.normal.y != 1' Does this just mean if its not completely flat?
			glideLockoutTimer = 0;
		}

		if (body == null || body.isKinematic) {
			return;
		}

		if (hit.moveDirection.y > -0.3) {
			return;
		}
		Vector3 pushDir = new Vector3 (hit.moveDirection.x, 0, hit.moveDirection.z);
		body.velocity = pushDir * pushPower;


		// Redundant Code
		/*
		if (hit.gameObject.tag == "Box") {
			// GameObject.FindGameObjectWithTag("Box").transform
		}
		if ((player.collisionFlags & CollisionFlags.Sides) != 0) {
			//print("touched the side");
			//verticalVelocity = 0;
		}
		if (!(player.collisionFlags == CollisionFlags.Below) && hit.controller.detectCollisions) {
			// print("fly");
		}
		//print((character.collisionFlags & CollisionFlags.Below) != 0);
		//print(hit.controller.detectCollisions);
		//print(hit.normal.y);
		//print(character.isGrounded);
		//print(character.collisionFlags);
		//Debug.Log(hit.moveDirection);
		*/
	}

	// Extra Interaction Stuff Below, Not Touching That
	void Climb ()
	{
		// If inside the ladder and pressing foward - climb. Foward overrides backward.
		if (inside == true && (Input.GetKey ("w") || Input.GetAxis ("X360_LStickY") < 0)) {
			ChController.transform.position += (Vector3.up * climbSpeed) * Time.deltaTime;
		} else if (inside == true && (Input.GetKey ("s") || Input.GetAxis ("X360_LStickY") > 0)) {
			ChController.transform.position += (Vector3.down * climbSpeed) * Time.deltaTime;
		}
	}

	void Interaction ()
	{
		if (Input.GetKey ("f") || Input.GetKey ("joystick button 2")) {
			interact = true;
		} else {
			interact = false;
		}

	}

	void OnTriggerStay (Collider Col)
	{
		//Debug.Log("Entered Trigger");

		if (Col.gameObject.tag == "Ladder") {
			//interaction();
			if (interact == true) {
				player.enabled = false;
				inside = true;
			}
			//currentLerpTime += Time.deltaTime; 
		}

		if (Col.gameObject.tag == "Exit") {
			Application.LoadLevel ("LadderPuzzle");
		}

		if (Col.gameObject.tag == "ExitLevel2") {
			player.transform.position = respawnPoint;
		}
	}

	void OnTriggerExit (Collider Col)
	{
		if (Col.gameObject.tag == "Ladder") {
			player.enabled = true;
			inside = false;
			interact = false;
		}
		/*
        if (currentLerpTime == lerpTime)
        {
            currentLerpTime = 0;
        }else
        {
            currentLerpTime = 0;
        }*/
	}

	public bool Grounded ()
	{ // What is this?
		return Physics.Raycast (player.transform.position, Vector3.down, distToGrounded, ground);
	}

	void OnCollisionEnter (Collision collisionInfo)
	{ // What is this?
		onGround = true;
	}

	void OnCollisionExit (Collision collisionInfo)
	{ // What is this?
		onGround = false;
	}

	// Redundant Code
	/*
	private float calculateDragForce() {
		return (player.velocity.y * player.velocity.y * dragCoefficient) / 2;
	}
	*/

	/*
    void onControllerColliderHit (ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if(body == null)
        {
          
        }
        //if(hit.moveDirection < -0.3f)
        {

        }
    }

	//Controls for gliding/flight with pitch
	void flightControl() {
		//character.Move((transform.forward * glideSpeed) * Time.deltaTime);

		float glideConst = 2.5f;

		Vector3 joystick = new Vector3(Input.GetAxis("X360_LStickX"), 0, Input.GetAxis("X360_LStickY"));

		transform.forward = joystick * Time.deltaTime * glideConst;
		if (Input.GetAxis("Horizontal") > 0 && Input.GetAxis("Horizontal") <= 1) {
			transform.forward = new Vector3(-joystick.x, 0, joystick.z) * Time.deltaTime * glideConst;
		}
		if (Input.GetAxis("Horizontal") < 0 && Input.GetAxis("Horizontal") >= -1) {
			transform.forward = new Vector3(-joystick.x, 0, joystick.z) * Time.deltaTime * glideConst;
		}
	}

    void turn() {
        //Press Q or left bumper button on XBOX 360 to turn left
        if (Input.GetKey(KeyCode.Q) || Input.GetKey("joystick button 4"))
        {
            
            characterRotation *= Quaternion.Euler(0,rotateVelocity * -1 * Time.deltaTime, 0);
            character.transform.rotation = characterRotation;
            //print(characterRotation);

        //Press E or right bumper button on XBOX 360 to turn right
        }else if (Input.GetKey(KeyCode.E) || Input.GetKey("joystick button 5"))
        {
            
            characterRotation *= Quaternion.Euler(0, rotateVelocity * 1 * Time.deltaTime, 0);
            //characterRotation *= Quaternion.AngleAxis(rotateVelocity * 1 * Time.deltaTime, Vector3.up);
            character.transform.rotation = characterRotation;
            //print(characterRotation);

        }
        /*
        Vector3 NextDir = new Vector3(Input.GetAxis("X360_RStickX"), 0, Input.GetAxis("X360_RStickY"));
        if (NextDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(NextDir);
        }*/
	//}
}

