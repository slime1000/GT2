using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public void OnDestroy()
    {

        Destroy(this.gameObject);
    }
}
