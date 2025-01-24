using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingEnemyNavigationScript : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    private float flightHeight = 10.5f;
    private float smoothRotationSpeed = 5f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        if (target == null)
        {
            GameObject baseTarget = GameObject.FindGameObjectWithTag("Base");
            if (baseTarget != null)
            {
                target = baseTarget.transform;
            }
        }
        
        Vector3 startPosition = transform.position;
        startPosition.y = flightHeight;
        transform.position = startPosition;
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 targetPosition = new Vector3(target.position.x, flightHeight, target.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.5f, speed);
        Vector3 direction = targetPosition - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * smoothRotationSpeed);
        }
    }
}
