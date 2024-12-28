using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private Transform target;
    public float range = 15f;
    public string EnemyTag = "zombie";
    public Transform movingPart;
    public float turningSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.25f);    
    }


    void UpdateTarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies){
            float distanceToTarget = Vector3.Distance (transform.position, enemy.transform.position);

            if(distanceToTarget < shortestDistance){
                shortestDistance = distanceToTarget;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range){
            target = nearestEnemy.transform;
        }
        else{
            target = null;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(target == null){
            return;
        }


        Vector3 direction = target.position - movingPart.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(movingPart.rotation, lookRotation, Time.deltaTime * turningSpeed).eulerAngles;
        movingPart.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
