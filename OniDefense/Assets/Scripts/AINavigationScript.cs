using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Game
{
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
            if (Vector3.Distance(gameObject.transform.position, objectivePoint.position) <= 2)
            {
                OnReachedDestination();
            }
        }

        void OnReachedDestination()
        {
            PlayerStats.DecreaseLives(GetComponent<EnemyBase>().damage);
            WaveSpawner.EnemyDied();
            Destroy(gameObject);
        }
    }
}

