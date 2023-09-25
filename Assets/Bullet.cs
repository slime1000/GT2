using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Shoot(Vector3 direction)
    {
      rb.AddForce(direction.normalized * 5, ForceMode.Impulse);
    }
}
