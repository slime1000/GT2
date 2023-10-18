using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Runtime.InteropServices;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonCharacterController : MonoBehaviour
{
    public Camera playerCamera;

    public float moveMaxSpeed = 5;
    public float moveAcceleration = 10;
    public float playerHealth = 5;
    public float jumpSpeed = 1;
    public float jumpMaxTime = 0.2f;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI healthText;
    public GameObject continueButton;
    public GameObject menuButton;
    public ParticleSystem growPart;
    public ParticleSystem hurtPart;

    private float jumpTimer = 0;

    private bool jumpInputPressed = false;
    private bool isJumping = false;
    private bool attackInputPressed = false;
    public bool isShrunk = false;
    public bool isPaused = false;

    private GameObject player;

    private CharacterController characterController;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentHorizontalVelocity = Vector2.zero;
    private Vector3 scaleChangeSmall, scaleChangeBig;
    

    private float currentVerticalVelocity = 0;


    private void Awake()
    {
        //disables pause ui
        pauseText.enabled = false;
        menuButton.SetActive(false);
        continueButton.SetActive(false);
        
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

        //health text update
        healthText.text = playerHealth.ToString();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == false)
            {
                //pauses and enables text
                isPaused = true;
                pauseText.enabled = true;
                continueButton.SetActive (true);
                menuButton.SetActive (true);
                Time.timeScale = 0;

            }
            else if (isPaused == true)
            {
                //unpauses and disables text
                isPaused = false;
                pauseText.enabled = false;
                menuButton.SetActive (false);
                continueButton.SetActive(false);
                Time.timeScale = 1;
            }
        }

        Vector3 cameraSpaceMovement = new Vector3(moveInput.x, 0, moveInput.y);
        cameraSpaceMovement = playerCamera.transform.TransformDirection(cameraSpaceMovement);
        //finds current horizontal movement
        Vector2 cameraHorizontalMovement = new Vector2(cameraSpaceMovement.x, cameraSpaceMovement.z);

        //finds horizontal movement
        currentHorizontalVelocity = Vector2.Lerp(currentHorizontalVelocity, cameraHorizontalMovement * moveMaxSpeed, Time.deltaTime * moveAcceleration);
        //sets current movement to the found horizontal and vertical
        Vector3 currentVelocity = new Vector3(currentHorizontalVelocity.x, currentVerticalVelocity, currentHorizontalVelocity.y);
        //gravity
        if (isJumping == false)
        {
            //sets gravity when jumping
            currentVerticalVelocity += Physics.gravity.y * Time.deltaTime;

            if (characterController.isGrounded && currentVerticalVelocity < 0)
            {
                //stops gravity when grounded
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

        //moves the character accordingly
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
            //if they are grounded, allow jump
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
                //do not jump if in air
                isJumping = false;
            }
        }
    }


    public void OnSize(InputValue value)
    {
        //This is not an attack, but it started as one so that is why some of my variables are named strangely.
        attackInputPressed = value.Get<float>() > 0;
        //checks ability input
       if(attackInputPressed)
        {
            Debug.Log("pressed!");
            if(isShrunk == false)
            {
                //play particle
                growPart.Play();
                //if player is big, makes them small, and lowers their jump power
                player.transform.localScale = scaleChangeSmall;
                Debug.Log("shrunk!");
                isShrunk = true;
                jumpSpeed = 2f;
            }
            else if(isShrunk == true)
            {
                //playparticle
                growPart.Play();
                //if player is small, resets size and makes jump normal
                player.transform.localScale = scaleChangeBig;
                Debug.Log("big!");
                isShrunk = false;
                jumpSpeed = 3f;
            }
        }
       
        }



	private void OnTriggerEnter(Collider collision)
	{
        //taking damage
        if (collision.gameObject.tag == "enemy")
        {
            //plays blood particle
            hurtPart.Play();
            playerHealth = playerHealth-1;
            Debug.Log("hit!");
        }
        if (playerHealth <= 0)
        {
            //reset scene on death
            SceneManager.LoadScene("3D");
        }
	}

    public void unpause()
    {
        //for continue button. same as unpause earlier, just in a function.
        isPaused = false;
        pauseText.enabled = false;
        menuButton.SetActive(false);
        continueButton.SetActive(false);
        Time.timeScale = 1;
    }

}
