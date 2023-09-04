using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{

    public float MoveSpeed;
    public InputActionAsset CharacterActionAsset;
    public Camera FirstPersonCamera;

    private InputAction moveAction;
    private InputAction rotateAction;
    private CharacterController characterController;
    private Vector2 moveValue;
    private Vector2 rotateValue;
    private Vector3 currentRotationAngle;
    private Vector3 secondaryRotationAngle;


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
    }
    // Update is called once per frame
    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>() * MoveSpeed * Time.deltaTime;
        rotateValue = rotateAction.ReadValue<Vector2>() * Time.deltaTime * 360;
        currentRotationAngle = new Vector3(currentRotationAngle.x - rotateValue.y, currentRotationAngle.y + rotateValue.x, 0);
        secondaryRotationAngle = new Vector3(0, currentRotationAngle.y + rotateValue.x, 0);
        currentRotationAngle = new Vector3(Mathf.Clamp(currentRotationAngle.x, -85, 85), currentRotationAngle.y, currentRotationAngle.z);
        FirstPersonCamera.transform.rotation = Quaternion.Euler(currentRotationAngle);
        characterController.transform.rotation = Quaternion.Euler(secondaryRotationAngle);
        characterController.Move(new Vector3(moveValue.x, 0, moveValue.y));

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Vector4(0, 1, 1, 0.5f);
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
