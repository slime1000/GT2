using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MoveToDest : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform destination;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        agent.destination = destination.position;
    }
}
