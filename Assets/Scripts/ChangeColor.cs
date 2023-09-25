using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color NewColor;
    public void ChangeLightColor()
    {
        GetComponent<Light>().color = NewColor;
    }
}
