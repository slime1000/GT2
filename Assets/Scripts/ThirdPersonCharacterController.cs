using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonCharacterController : MonoBehaviour
{
    public float moveMaxSpeed = 5;
    public float moveAcceleration = 10;

    public float jumpSpeed = 1;
    public float jumpMaxTime = 0.2f;
    private float jumpTimer = 0;

    private bool jumpInputPressed = false;
    private bool isJumping = false;

    private CharacterController characterController;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentHorizontalVelocity = Vector2.zero;

    private float currentVerticalVelocity = 0;

    public bullet bullet;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        currentHorizontalVelocity = Vector2.Lerp(currentHorizontalVelocity, moveInput * moveMaxSpeed, Time.deltaTime * moveAcceleration);
        Vector3 currentVelocity = new Vector3(currentHorizontalVelocity.x, currentVerticalVelocity, currentHorizontalVelocity.y);

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
            jumpTimer += Time.deltaTime;

            if (jumpTimer >= jumpMaxTime)
            {
                isJumping = false;
            }
        }


        Vector3 horizontalDirection = Vector3.Scale(currentVelocity, new Vector3(1, 0, 1));
        if (currentVelocity.magnitude > 0.0001)
        {
            Quaternion newDirection = Quaternion.LookRotation(horizontalDirection.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * moveAcceleration);
        }


        characterController.Move(currentVelocity * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
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


    public void OnAttack()
    {
        Collider[] overlapItems = Physics.OverlapBox(transform.position, Vector3.one);

        if (overlapItems.Length > 0)
        {
            foreach (Collider item in overlapItems)
            {
                Vector3 direction = item.transform.position - transform.position;
                item.SendMessage("OnPlayerAttack", direction, SendMessageOptions.DontRequireReceiver);
            }
        }

        GameObject bulletCopy = Instantiate(bullet.gameObject);
        bulletCopy.transform.position = transform.forward;
        bulletCopy.GetComponent<bullet>().Shoot(new Vector3(currentHorizontalVelocity.x, 0, currentHorizontalVelocity.y));

}
