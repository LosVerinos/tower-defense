using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigationScript : MonoBehaviour
{
    public Transform objectivePoint;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = objectivePoint.position;
        if (Vector3.Distance(transform.position, objectivePoint.position) < 1f)
        {
            Destroy(gameObject);
            WaveManagerScript.EnemyDied();
        }
    }
}
