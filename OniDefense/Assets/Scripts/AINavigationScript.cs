using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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

    void Update()
    {
        // Vérifier si le zombie est arrivé à destination
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                // Le zombie est arrivé à destination
                OnReachedDestination();
            }
        }
    }

    void OnReachedDestination()
    {
        // Décrémenter les vies
        LivesManager.Instance.DecreaseLives(1);
        WaveManagerScript.EnemyDied();
        // Tuer le zombie
        Destroy(gameObject);
    }
}
