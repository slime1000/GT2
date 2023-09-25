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
        animator.SetBool("showInteractionPrompt", showPrompt);
    }


}
