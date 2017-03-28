using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	// Character
	public CharacterController player;
	// Needs "Plume" assigned in inspector.
	private Transform playerTransform;

    public bool isLookingAtPlayer;

    public LayerMask playerLayer;

	// Camera
	public Camera playerCamera;
	// Needs "Main Camera" assigned in inspector.
	private Transform cameraTransform;

	// Input
	public float inputX = 0f;
	public float inputY = 0f;

	// Angle Constraints
	public const float Y_ANGLE_MIN = -85f;
	//-89.9 keeps the camera from flipping over the vertical axis.
	public const float Y_ANGLE_MAX = -10f;

	// Camera Placement
	// Leave at -20
	public float startingHeight = -20f;
	// This influences the clamp angles.
	public float distance = 4f;
	// Depending on the character's origin
	public Vector3 offset = new Vector3 (0f, 1.25f, 0f); //1.25f
    private Rigidbody rb;
    private Vector3 wantedPosition;


    // Redundant Code
    /*
	public Transform player;
	public float cameraSpeed = 15;
	public float zoomSpeed = 20;
	public float minZoom = .5;
	public float yOffset = 5;
	public float xOffset = 6;
	public float zOffset = 10;
	public bool smoothFollow = true;
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;
    public Vector3 mousePos = Input.mousePosition;
	float RStickX;
	float RStickY;
	*/

    void Start ()
	{
		// Character
		playerTransform = player.transform; // Despite being in Start(), characterTransform stays updated with the character's current transform values. Works, but don't know how.
        rb = GetComponent<Rigidbody>();
        // Camera
        cameraTransform = playerCamera.transform;
		inputY += startingHeight;
        isLookingAtPlayer = true;
		//PlayerCamera = GetComponent<Camera>();
	}

	void Update ()
	{
        isPlayerLookedAt();
        // Mouse Input
        inputX += Input.GetAxis ("Mouse X");
		inputY += Input.GetAxis ("Mouse Y");

		// Controller Input
		inputX += Input.GetAxis ("X360_RStickX") * 2;
		inputY += -Input.GetAxis ("X360_RStickY") * 2;

		// Angle Limiter
		inputY = Mathf.Clamp (inputY, Y_ANGLE_MIN, Y_ANGLE_MAX);

		// Redundant Code
		/*
		RStickX = Input.GetAxis("X360_RStickX");
		RStickY = Input.GetAxis("X360_RStickY");
		*/

		/*
		Vector3 nextDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

		if (nextDirection != Vector3.zero) {
			//transform.rotation = Quaternion.LookRotation(NextDir);
		}
		*/

		/*
        if (player) {
            //Vector3 newPos = transform.position;
            Vector3 newPos = mousePos;
       
            newPos.x = player.position.x - xOffset;
            newPos.y = player.position.y + yOffset;
            newPos.z = player.position.z - zOffset;
       

            if (!smoothFollow)
            {
                transform.position = newPos;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, newPos, cameraSpeed * Time.deltaTime);
            }
        }*/
	}

	void LateUpdate ()
	{
        if (isLookingAtPlayer == true)
        {
            // Default camera distance and height.
            Vector3 direction = new Vector3(0, 0, distance); // This needs to be in LateUpdate() for some reason. Works, don't know how.

            // Convert input into a quaternion rotation.
            Quaternion rotation = Quaternion.Euler(inputY, inputX, 0); // "Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order)."

            // Sets camera position using charactetr postion + rotated vector.
            cameraTransform.position = playerTransform.position + (rotation * direction); // Multiplying Quaternion rotation by Vector3 direction effectively rotates the vector
                                                                                          // Sets the camera to look at the player's position.
            cameraTransform.LookAt(playerTransform.position + offset); //+ offset
        }

        else if(isLookingAtPlayer == false)
        {
            Vector3 direction = new Vector3(0, 0, distance);
            //Quaternion rotation = Quaternion.Euler(-wantedPosition.y, -wantedPosition.x, 0);
            //cameraTransform.position = playerTransform.position + (rotation * direction);
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, playerTransform.position + Vector3.up * distance, Time.deltaTime); //playerTransform.position + Vector3.up*5
            cameraTransform.LookAt(playerTransform.position + offset); //+ offset
                

        }
       // Debug.Log(Time.realtimeSinceStartup + isLookingAtPlayer.ToString());
        // Where is cameraTransform ever applied to the actual main camera?
    }

    // Redundant Code
    /*
    void CameraZoom() {
        playerCamera.fieldOfView -= zoomSpeed / 4;

        if (playerCamera.fieldOfView < minZoom) {
            playerCamera.fieldOfView = minZoom;
        }
    }
    */

    void isPlayerLookedAt() {
        RaycastHit test;
        Ray rayTest = Camera.main.ViewportPointToRay(Vector3.one * 0.5f - Vector3.forward * 0.5f);
        Debug.DrawRay(rayTest.origin, rayTest.direction, Color.red);
        if(Physics.Raycast(rayTest, out test, 500.0f))
        {
           // Debug.LogWarning(test.collider.name);
        }

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.yellow);
        RaycastHit hitCam;
        Ray rayToCam = new Ray(playerTransform.position, cameraTransform.position);
        Debug.DrawRay(playerTransform.position, cameraTransform.position, Color.cyan);
        if (Physics.Raycast(ray, out hit) && Physics.Raycast(rayToCam, out hitCam))
        {
            Debug.LogWarning(test.collider.name);
            //isLookingAtPlayer = (hit.collider.gameObject.layer == playerLayer);
            if ((hit.collider.gameObject.layer == playerLayer 
                || hit.collider.gameObject.tag == "Player" 
                || hit.collider.name == "HeadB"
                || hit.collider.name == "BodyP"))
            {
                isLookingAtPlayer = true;
            }

            else {
                isLookingAtPlayer = true;
                wantedPosition = hit.point;

            }
        }
       
    }
    
}
