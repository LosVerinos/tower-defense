using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class TurretScript : MonoBehaviour
    {
        private Transform target;

        [Header("Paramètres Tourelle")]
        public float maximumRange = 15f;
        public float minimumRange = 0f;
        public float turningSpeed = 10f;
        public float damages;
        public float fireRate;
        private float fireCountdown;

        [Header("Paramètres Unity")]
        //public List<string> enemyTags = new List<string> { "Classic Enemy" };
        public LayerMask enemyLayer;
        public Transform movingPartY;
        public Transform movingPartX;
        public GameObject bulletPrefab;
        public Transform firePoint;
        public GameObject muzzleFlash;
        private GameObject nearestEnemy = null;
        private Quaternion defaultRotationX;
        private Quaternion defaultRotationY;
        private bool active = false;

        void Start()
        {
            defaultRotationX = movingPartX.rotation;
            defaultRotationY = movingPartY.rotation;
            InvokeRepeating("UpdateTarget", 0f, 0.25f);
        }

        public void Initialize()
        {
            fireCountdown = 0f;
            target = null; // Ensure the turret starts with no target
            Debug.Log("Turret initialized");
        }

        bool UpdateTarget()
        {
            
            
            if (!active) return false;
            if(nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.transform.position) >= minimumRange && Vector3.Distance(transform.position, nearestEnemy.transform.position) <= maximumRange){
                Debug.Log(Vector3.Distance(transform.position, nearestEnemy.transform.position) + " / "+ maximumRange);
                target = nearestEnemy.transform;
                return true;
            }
            else{
                nearestEnemy = null;
                target = null;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, maximumRange, enemyLayer);
            float shortestDistance = Mathf.Infinity;

            foreach (Collider collider in colliders)
            {
                float distanceToTarget = Vector3.Distance(transform.position, collider.transform.position);
                if (distanceToTarget >= minimumRange && distanceToTarget < shortestDistance)
                {
                    shortestDistance = distanceToTarget;
                    nearestEnemy = collider.gameObject;
                }
            }

            if (nearestEnemy != null)
            {
                target = nearestEnemy.transform;
                return true;
            }
            else
            {
                target = null;
                return false;
            }
        }


        void Update()
        {
            if (!active) return;

            if (!UpdateTarget() && target == null)
            {
                // Retour progressif à la position par défaut
                Vector3 rotationX = Quaternion.Lerp(movingPartX.rotation, defaultRotationX, Time.deltaTime * turningSpeed).eulerAngles;
                Vector3 rotationY = Quaternion.Lerp(movingPartY.rotation, defaultRotationY, Time.deltaTime * turningSpeed).eulerAngles;
                movingPartX.rotation = Quaternion.Euler(rotationX.x, rotationY.y, 0f);
                movingPartY.rotation = Quaternion.Euler(0f, rotationY.y, 0f);
                return;
            }
            else
            {
                // Suivi de la cible
                Vector3 direction = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z) - movingPartY.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Vector3 rotationY = Quaternion.Lerp(movingPartY.rotation, lookRotation, Time.deltaTime * turningSpeed).eulerAngles;
                Vector3 rotationX = Quaternion.Lerp(movingPartX.rotation, lookRotation, Time.deltaTime * turningSpeed).eulerAngles;
                movingPartY.rotation = Quaternion.Euler(0f, rotationY.y, 0f);
                movingPartX.rotation = Quaternion.Euler(rotationX.x, rotationY.y, 0f);
            }

            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1f / (fireRate * PlayerStats.fireRateMultiplier);
            }

            fireCountdown -= Time.deltaTime;
        }

        void Shoot()
        {
            if (!active) return;

            GameObject flash = Instantiate(muzzleFlash, firePoint.position, firePoint.rotation);
            Destroy(flash, 0.2f);

            GameObject bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            BulletScript bulletScript = bulletGameObject.GetComponent<BulletScript>();

            if (bulletScript != null)
            {
                bulletScript.SetDamage(damages);
                bulletScript.Find(target);
            }
            else
            {
                ObusScript obusScript = bulletGameObject.GetComponent<ObusScript>();
                obusScript.SetDamage(damages);
                obusScript.Find(target);
            }

            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        public void SetActive(bool _active)
        {
            active = _active;
        }
    }

}
