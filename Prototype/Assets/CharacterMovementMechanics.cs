using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementMechanics : MonoBehaviour
{

    public CharacterController character;

    public float forwardVelocity;
    public float backwardVelocity;
    public float rightVelocity;
    public float leftVelocity;
    public float acceleration;
    public float deceleration;
    public float maxVelocity;
    public float rotateVelocity;

    public float playerGravity;
    public float verticalVelocity;

    // Use this for initialization
    void Start()
    {

        //Character initialization
        character = GetComponent<CharacterController>();

        forwardVelocity = 0f;
        backwardVelocity = 0f;
        rightVelocity = 0f;
        leftVelocity = 0f;

        acceleration = 3f;
        deceleration = .5f;
        rotateVelocity = 200f;
        maxVelocity = 3f;

        verticalVelocity = 0f;
        playerGravity = -9.80f;

    }

    // Update is called once per frame
    void Update()
    {
        walk();
    }


    void walk()
    {

        //Up arrow and W with acceleration
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            forwardVelocity += (acceleration);
        }

        //Down arrow and s with acceleration
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            backwardVelocity += acceleration;
        }

        //Right arrow and D with acceleration
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rightVelocity += acceleration;
        }

        //Left arrow and A with acceleration
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            leftVelocity += acceleration;
        }

        //Deceleration for forward, backward, right, and left velocity
        forwardVelocity -= deceleration;
        forwardVelocity = Mathf.Clamp(forwardVelocity, 0, maxVelocity);
        backwardVelocity -= deceleration;
        backwardVelocity = Mathf.Clamp(backwardVelocity, 0, maxVelocity);
        rightVelocity -= deceleration;
        rightVelocity = Mathf.Clamp(rightVelocity, 0, maxVelocity);
        leftVelocity -= deceleration;
        leftVelocity = Mathf.Clamp(leftVelocity, 0, maxVelocity);

        verticalVelocity = character.velocity.y;

        verticalVelocity += playerGravity * Time.deltaTime;
        //jump();
        //glide();
        //Apply the directional movement to the character
        //character.Move(((Vector3.forward * forwardVelocity) + (Vector3.back * backwardVelocity) + (Vector3.right * rightVelocity) + (Vector3.left * leftVelocity)) * Time.deltaTime);
        character.Move(((Camera.main.transform.forward * forwardVelocity) + (-Camera.main.transform.forward * backwardVelocity) + (Camera.main.transform.right * rightVelocity) + (-Camera.main.transform.right * leftVelocity)) * Time.deltaTime);
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

    void OnTriggerEnter(Collider Col)
    {
        //Debug.Log("In");
    }
}