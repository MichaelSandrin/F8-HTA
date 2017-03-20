using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	// Character
	public CharacterController player;
	// Needs "Plume" assigned in inspector.
	private Transform playerTransform;

	// Camera
	public Camera playerCamera;
	// Needs "Main Camera" assigned in inspector.
	private Transform cameraTransform;

	// Input
	public float inputX = 0f;
	public float inputY = 0f;

	// Angle Constraints
	public const float Y_ANGLE_MIN = -70f;
	//-89.9 keeps the camera from flipping over the vertical axis.
	public const float Y_ANGLE_MAX = 70f;

	// Camera Placement
	// Leave at -20
	public float startingHeight;
	// This influences the clamp angles.
	public float distance = 4f;
	// Depending on the character's origin
	public Vector3 offset;


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

		// Camera
		cameraTransform = playerCamera.transform;
		inputY += startingHeight;
		//PlayerCamera = GetComponent<Camera>();
	}

	void Update ()
	{
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
		// Default camera distance and height.
		Vector3 direction = new Vector3 (0, 0, distance); // This needs to be in LateUpdate() for some reason. Works, don't know how.

		// Convert input into a quaternion rotation.
		Quaternion rotation = Quaternion.Euler (inputY, inputX, 0); // "Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis (in that order)."

		// Sets camera position using charactetr postion + rotated vector.
		cameraTransform.position = playerTransform.position + (rotation * direction); // Multiplying Quaternion rotation by Vector3 direction effectively rotates the vector

		// Sets the camera to look at the player's position.
		cameraTransform.LookAt (playerTransform.position + offset);

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
}
