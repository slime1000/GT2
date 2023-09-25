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
        if (MyManager.Instance.playerScore >= 5)
        {
            Destroy(this.gameObject);
        }
    }
   
}
