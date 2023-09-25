using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonInteraction : MonoBehaviour
{
    public UnityEvent onButtonPress;
    public void OnPlayerInteract()
    {
        Debug.Log("hit");
        onButtonPress.Invoke();
    }
}
