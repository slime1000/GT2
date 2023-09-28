using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterController))]
public class ThirdPersonCharacterController : MonoBehaviour
{
    public Camera playerCamera;

    public float moveMaxSpeed = 5;
    public float moveAcceleration = 10;
    public float playerHealth = 5;
    public float jumpSpeed = 1;
    public float jumpMaxTime = 0.2f;

    private float jumpTimer = 0;

    private bool jumpInputPressed = false;
    private bool isJumping = false;
    private bool attackInputPressed = false;
    public bool isShrunk = false;

    private GameObject player;

    private CharacterController characterController;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentHorizontalVelocity = Vector2.zero;
    private Vector3 scaleChangeSmall, scaleChangeBig;
    

    private float currentVerticalVelocity = 0;


    private void Awake()
    {
        //setting player gameobject for scale
        player = this.gameObject;
        scaleChangeSmall = new Vector3(0.5f, 0.5f, 0.5f);
        scaleChangeBig = new Vector3(1f, 1f, 1f);
        //getting component
        characterController = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraSpaceMovement = new Vector3(moveInput.x, 0, moveInput.y);
        cameraSpaceMovement = playerCamera.transform.TransformDirection(cameraSpaceMovement);
        Vector2 cameraHorizontalMovement = new Vector2(cameraSpaceMovement.x, cameraSpaceMovement.z);

        currentHorizontalVelocity = Vector2.Lerp(currentHorizontalVelocity, cameraHorizontalMovement * moveMaxSpeed, Time.deltaTime * moveAcceleration);
        Vector3 currentVelocity = new Vector3(currentHorizontalVelocity.x, currentVerticalVelocity, currentHorizontalVelocity.y);
        //gravity
        if (isJumping == false)
        {
            currentVerticalVelocity += Physics.gravity.y * Time.deltaTime;

            if (characterController.isGrounded && currentVerticalVelocity < 0)
            {
                  currentVerticalVelocity = Physics.gravity.y * Time.deltaTime; 
            }
        }
        else
        {
            //jump height
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpMaxTime)
            {
                isJumping = false;
            }
        }

        //direction facing
        Vector3 horizontalDirection = Vector3.Scale(currentVelocity, new Vector3(1, 0, 1));
        if (horizontalDirection.magnitude > 0.0001)
        {
            Quaternion newDirection = Quaternion.LookRotation(horizontalDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * moveAcceleration);
        }


        characterController.Move(currentVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        //setting move input
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        //jumping
        jumpInputPressed = value.Get<float>() > 0;

        if (jumpInputPressed)
        {
            if (characterController.isGrounded)
            {
                isJumping = true;

                jumpTimer = 0;

                currentVerticalVelocity = jumpSpeed;
            }
        }
        else
        {
            if (isJumping)
            {
                isJumping = false;
            }
        }
    }


    public void OnSize(InputValue value)
    {
        //I am making the "attack" a change in the player's size.
        attackInputPressed = value.Get<float>() > 0;
       if(attackInputPressed)
        {
            Debug.Log("pressed!");
            if(isShrunk == false)
            {
                player.transform.localScale = scaleChangeSmall;
                Debug.Log("shrunk!");
                isShrunk = true;
            }
            else if(isShrunk == true)
            {
                player.transform.localScale = scaleChangeBig;
                Debug.Log("big!");
                isShrunk = false;
            }
        }
       
        }



	private void OnTriggerEnter(Collider collision)
	{
        //taking damage and restarting
        if (collision.gameObject.tag == "enemy")
        {
            playerHealth = playerHealth-1;
            Debug.Log("hit!");
        }
        if (playerHealth <= 0)
        {
            SceneManager.LoadScene("3D");
        }
	}

}
