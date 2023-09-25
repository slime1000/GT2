using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    public UnityEvent FiveEggs;
    public UnityEvent OnEggTouch;
    public float MoveSpeed;
    public InputActionAsset CharacterActionAsset;
    public Camera FirstPersonCamera;
    public LayerMask GroundLayer;
    public float MouseSensitivity = 5;
    public float MaxJumpHeight = 1;
    public float GroundCheckRadius = 0.25f;
    public Transform GroundCheck;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction rotateAction;
    private CharacterController characterController;
    private Vector2 moveValue;
    private Vector2 rotateValue;
    private Vector3 currentRotationAngle;
    private Vector3 secondaryRotationAngle;
    private float verticalMovement = 0;
    private bool isGrounded = false;
    private bool isJumping = false;


    private void OnEnable()
    {
        CharacterActionAsset.FindActionMap("Gameplay").Enable();
    }

    private void OnDisable()
    {
        CharacterActionAsset.FindActionMap("Gameplay").Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();

        moveAction = CharacterActionAsset.FindActionMap("Gameplay").FindAction("Move");
        rotateAction = CharacterActionAsset.FindActionMap("Gameplay").FindAction("Look");
        jumpAction = CharacterActionAsset.FindActionMap("Gameplay").FindAction("Jump");
    }
    // Update is called once per frame
    void Update()
    {

       if (Input.GetKeyDown(KeyCode.R))
		{
            SceneManager.LoadScene("Blockout");

        }


        moveValue = moveAction.ReadValue<Vector2>() * MoveSpeed * Time.deltaTime;
        rotateValue = rotateAction.ReadValue<Vector2>() * Time.deltaTime * 360;
        rotateValue = Vector2.ClampMagnitude(rotateValue, MouseSensitivity);
        currentRotationAngle = new Vector3(currentRotationAngle.x - rotateValue.y, currentRotationAngle.y + rotateValue.x, 0);
        secondaryRotationAngle = new Vector3(0, currentRotationAngle.y + rotateValue.x, 0);
        currentRotationAngle = new Vector3(Mathf.Clamp(currentRotationAngle.x, -85, 85), currentRotationAngle.y, currentRotationAngle.z);
        FirstPersonCamera.transform.rotation = Quaternion.Euler(currentRotationAngle);
        characterController.transform.rotation = Quaternion.Euler(secondaryRotationAngle);
        Vector3 movementToApply = transform.TransformDirection(new Vector3(moveValue.x, 0, moveValue.y));
        characterController.Move((movementToApply * Time.deltaTime));

        ProcessVerticalMovementInput();

    }

    private void ProcessVerticalMovementInput()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundCheckRadius, GroundLayer);
        if (characterController.isGrounded && verticalMovement < 0)
        {
            Debug.Log("grounded");
            isJumping = false;

            verticalMovement = 0;
           
        }

        if (isGrounded && !isJumping) 
        {
            bool jumpButtonDown = jumpAction.triggered && jumpAction.ReadValue<float>() > 0;

            if (jumpButtonDown)
            {
                float jumpForce = Mathf.Sqrt(-2 * MaxJumpHeight * Physics.gravity.y);
                Debug.Log("jumped");
                isJumping = true;

                verticalMovement += jumpForce;
            }
        }
        
        verticalMovement += Physics.gravity.y * Time.deltaTime;
        characterController.Move(Vector3.up * verticalMovement * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(0, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEggTouch.Invoke();
        MyManager.Instance.AddScore();
    }
}
