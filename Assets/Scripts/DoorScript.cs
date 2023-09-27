using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //removes the end door when the player has collected all 5 eggs.
        if (MyManager.Instance.playerScore >= 5)
        {
            Destroy(this.gameObject);
        }
    }
   
}
