using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]


public class UIAnimManager : MonoBehaviour
{
   public static UIAnimManager instance;

    private Animator animator;

    private void Awake()
    {
        //making manager and if there are more than 1 fix that
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }

        animator = GetComponent<Animator>();
    }


    private void OnDisable()
    {
        if(instance == this)
        {
            instance = null;
        }
    }

    public void ShowInteractPrompt(bool showPrompt)
    {
        //set ui on or off.
        animator.SetBool("showInteractionPrompt", showPrompt);
    }


}
