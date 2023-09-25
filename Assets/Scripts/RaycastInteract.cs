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
        interactAction = CharacterActionAsset.FindActionMap("Gameplay").FindAction("Interact");
    }
    void Update()
    {
        Ray myInteractionRay = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit interactionHitInfo;

        bool interactInputPressed = interactAction.triggered && interactAction.ReadValue<float>() > 0;

        UIAnimManager.instance.ShowInteractPrompt(false);

            if (Physics.Raycast(myInteractionRay, out interactionHitInfo, distance))
            {
               
                    if (interactionHitInfo.transform.tag == "Interactable")
                    {
                         UIAnimManager.instance.ShowInteractPrompt(true);
                             if (interactInputPressed)
                             {
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
