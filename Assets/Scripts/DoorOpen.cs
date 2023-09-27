using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    //destroys the door (wow!)
    public void OnDestroy()
    {

        Destroy(this.gameObject);
    }
}
