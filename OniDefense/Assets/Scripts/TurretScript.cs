using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private Transform target;
    public float maximumRange = 15f;
    public float minimumRange = 0f;
    public string EnemyTag = "zombie";
    public Transform movingPart;
    public float turningSpeed = 10f;
    private Quaternion defaultRotation;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = movingPart.rotation;
        InvokeRepeating("UpdateTarget", 0f, 0.25f);    
    }


    void UpdateTarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemyInRange = null;

        foreach (GameObject enemy in enemies){
            float distanceToTarget = Vector3.Distance (transform.position, enemy.transform.position);

            if(distanceToTarget >= minimumRange && distanceToTarget < shortestDistance){
                shortestDistance = distanceToTarget;
                nearestEnemyInRange = enemy;
            }
        }

        if(nearestEnemyInRange != null && shortestDistance <= maximumRange && shortestDistance >= minimumRange){
            target = nearestEnemyInRange.transform;
        }
        else{
            target = null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Vector3 rotation = Quaternion.Lerp(movingPart.rotation, defaultRotation, Time.deltaTime * turningSpeed).eulerAngles;
            movingPart.rotation = Quaternion.Euler(rotation);
            return;
        }
        else{
            Vector3 direction = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z) - movingPart.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(movingPart.rotation, lookRotation, Time.deltaTime * turningSpeed).eulerAngles;
            movingPart.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maximumRange);
        Gizmos.DrawWireSphere(transform.position, minimumRange);
    }
}
