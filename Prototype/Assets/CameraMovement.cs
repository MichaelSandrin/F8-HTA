using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform player;
    private Camera PlayerCamera;
    public float cameraSpeed = 15;
    public float zoomSpeed = 20f;
    public float minZoom = .5f;
    public float yOffset = 5f;
    public float xOffset = 6f;
    public float zOffset = 10f;
    public bool smoothFollow = true;

    public const float Y_ANGLE_MIN = -0.0f;
    public const float Y_ANGLE_MAX = 45.0f;

    CharacterController character;
    public Transform lookAt;
    public Transform camTransform;


    public float distance = 10f;
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    public float sensitivityX = 4.0f;
    public float sensitivityY = 1.0f;

    float RStickX;
    float RStickY;

    public Vector3 mousePos = Input.mousePosition;


    // Use this for initialization
    void Start()
    {
        //PlayerCamera = GetComponent<Camera>();
        character = GetComponent<CharacterController>();
        lookAt = character.transform;
        camTransform = Camera.main.transform;
        PlayerCamera = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 NextDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (NextDir != Vector3.zero)
        {
            //transform.rotation = Quaternion.LookRotation(NextDir);
        }
            
     

        RStickX = Input.GetAxis("X360_RStickX");
        RStickY = Input.GetAxis("X360_RStickY");

        print(-RStickX);
        print(RStickY);

        if (-Input.GetAxis("X360_RStickX") >= 0)
        {
            currentX += -Input.GetAxis("X360_RStickX");
            transform.Rotate(new Vector3(0, -Input.GetAxis("X360_RStickX"), 0));
        }
        else if (-Input.GetAxis("X360_RStickX") <= 0 || -Input.GetAxis("X360_RStickX") <= 0)
        {
            currentX += -Input.GetAxis("X360_RStickX");
            transform.Rotate(new Vector3(0, -Input.GetAxis("X360_RStickX"), 0));
        }

        if(-Input.GetAxis("Mouse X") >= 0) 
        {
            currentX += -Input.GetAxis("Mouse X");
            transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0));
        }
        else if (-Input.GetAxis("Mouse X") <= 0)
        {
            currentX += -Input.GetAxis("Mouse X");
            transform.Rotate(new Vector3(0, -Input.GetAxis("Mouse X"), 0));
        }


        if (-Input.GetAxis("X360_RStickX") <= 0)
        {
            currentY += -Input.GetAxis("X360_RStickY");
        }
        else if (-Input.GetAxis("X360_RStickX") >= 0)
        {
            currentY += -Input.GetAxis("X360_RStickY");
        }
        
        if(-Input.GetAxis("Mouse Y") >= 0)
        {
            currentY += -Input.GetAxis("Mouse Y");
        }else if(-Input.GetAxis("Mouse Y") <= 0)
        {
            currentY += -Input.GetAxis("Mouse Y");
        }

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        /*
        if (player)
        {
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

    void CameraZoom()
    {
        PlayerCamera.fieldOfView -= zoomSpeed / 4;
        if(PlayerCamera.fieldOfView < minZoom)
        {
            PlayerCamera.fieldOfView = minZoom;
        }

    }
}
