using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventExample : MonoBehaviour
{
    public UnityEvent OnCubeTouch;

    private void OnTriggerEnter(Collider other)
    {
        //when touching cube activate function
        OnCubeTouch.Invoke();
    }
}
