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
        //gets the navmesh agent and sets it
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        //sets destination to the players location
        agent.destination = destination.position;
    }
}
