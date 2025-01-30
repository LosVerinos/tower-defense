using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigationScript : MonoBehaviour
{
    public Transform objectivePoint;
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = objectivePoint.position;
        
    }
}
