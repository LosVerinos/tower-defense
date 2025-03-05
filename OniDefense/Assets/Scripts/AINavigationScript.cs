using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AINavigationScript : MonoBehaviour
{
    public Transform objectivePoint;
    public NavMeshAgent agent;
    public float speed = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = objectivePoint.position;
    }

    void Update()
    {
        // Vérifier si le zombie est arrivé à destination
        if (Vector3.Distance(gameObject.transform.position, objectivePoint.position) <= 2)
        {
            OnReachedDestination();
        }
    }

    void OnReachedDestination()
    {
        // Décrémenter les vies
        PlayerStats.DecreaseLives(GetComponent<EnemyBase>().damage);
        WaveSpawner.EnemyDied();
        // Tuer le zombie
        Destroy(gameObject);
    }
}
