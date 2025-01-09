using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{

    private Transform target;

    [Header("Parametres tourelle")]
    public float maximumRange = 15f;
    public float minimumRange = 0f;
    public float turningSpeed = 10f;
    public float damages;
    public float fireRate;
    private float fireCountdown;

    [Header("Parametres Unity")]
    public string EnemyTag = "zombie";
    public Transform movingPartY;
    public Transform movingPartX;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public GameObject muzzleFlash;
    
    private Quaternion defaultRotationX;
    private Quaternion defaultRotationY;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        defaultRotationX = movingPartX.rotation;
        defaultRotationY = movingPartY.rotation;
        InvokeRepeating("UpdateTarget", 0f, 0.25f);    
    }


    bool UpdateTarget(){
        if(active){
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
                return false; 
            }
            return true;
        }
        else
            return false;
    }


    // Update is called once per frame
    void Update()
    {
        if(active){
            if(target == null && !UpdateTarget()){
                Vector3 rotationX = Quaternion.Lerp(movingPartX.rotation, defaultRotationX, Time.deltaTime * turningSpeed).eulerAngles;
                Vector3 rotationY = Quaternion.Lerp(movingPartY.rotation, defaultRotationY, Time.deltaTime * turningSpeed).eulerAngles;
                movingPartX.rotation = Quaternion.Euler(rotationX.x, rotationY.y, 0f);
                movingPartY.rotation = Quaternion.Euler(0f, rotationY.y, 0f);
                return;
            }
            else{
                Vector3 direction = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z) - movingPartY.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Vector3 rotationY = Quaternion.Lerp(movingPartY.rotation, lookRotation, Time.deltaTime * turningSpeed).eulerAngles;
                Vector3 rotationX = Quaternion.Lerp(movingPartX.rotation, lookRotation, Time.deltaTime * turningSpeed).eulerAngles;
                movingPartY.rotation = Quaternion.Euler(0f, rotationY.y, 0f);
                movingPartX.rotation = Quaternion.Euler(rotationX.x, rotationY.y, 0f);


                /* //No need 
                if(movingPartX.name != movingPartY.name){
                    movingPartY.rotation = Quaternion.Euler(0f, rotationY.y, 0f);
                }
                */
            }


            if(fireCountdown <= 0){
                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, maximumRange);
        Gizmos.DrawWireSphere(transform.position, minimumRange);
    }

    void Shoot(){
        if(active){
            GameObject flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            Destroy(flash, 0.2f);
            GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript bulletScript = bulletGameObject.GetComponent<BulletScript>();
            bulletScript.SetDamage(damages);

            if(bulletScript != null){
                bulletScript.Find(target);
            }
        }
    }

    public void SetActive(bool _active){
        active = _active;
    }
}
