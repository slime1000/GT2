using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInteract : MonoBehaviour
{
    public float distance = 1;
    public InputActionAsset CharacterActionAsset;
    public Camera playerCamera;


    private InputAction interactAction;


    //enabling and disabling action map
    private void OnEnable()
    {
        CharacterActionAsset.FindActionMap("Gameplay").Enable();
    }

    private void OnDisable()
    {
        CharacterActionAsset.FindActionMap("Gameplay").Disable();
    }

    private void Awake()
    {
        //setting interact action
        interactAction = CharacterActionAsset.FindActionMap("Gameplay").FindAction("Interact");
    }
    void Update()
    {
        //making raycast
        Ray myInteractionRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit interactionHitInfo;

        //check if interact is pressed
        bool interactInputPressed = interactAction.triggered && interactAction.ReadValue<float>() > 0;

        //turn off ui for interact
        UIAnimManager.instance.ShowInteractPrompt(false);

            if (Physics.Raycast(myInteractionRay, out interactionHitInfo, distance))
            {
               //if near interactable, enable ui
                    if (interactionHitInfo.transform.tag == "Interactable")
                    {
                         UIAnimManager.instance.ShowInteractPrompt(true);
                             if (interactInputPressed)
                             {
                    //sending the interaction to the button
                                interactionHitInfo.transform.SendMessage("OnPlayerInteract");
                             }
                    }
              
            }
        
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
        Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward);
    }
}
