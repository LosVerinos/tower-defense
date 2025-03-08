using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class ObusScript : Projectile
    {
        public float damagesRadius;
        private Vector3 destination;
        private Vector3 startPosition;
        private float flightTime;
        private float elapsedTime;
        public float arcHeight; // Hauteur maximale de l'obus
        private Vector3 previousPosition;
        private bool aerialLaunch = false;

        public new void Find(Transform _target)
        {
            base.Find(_target);

            destination = new Vector3(target.position.x, 0, target.position.z);
            startPosition = transform.position;
            float distance = Vector3.Distance(startPosition, destination);
            flightTime = distance / speed;

            elapsedTime = 0f;

            previousPosition = startPosition;
        }

        protected override void Update()
        {
            if (!aerialLaunch)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= flightTime || transform.position.y < 0f)
                {
                    Explode();
                    return;
                }

                float progress = elapsedTime / flightTime;
                Vector3 horizontalPosition = Vector3.Lerp(startPosition, destination, progress);

                float verticalOffset = Mathf.Sin(progress * Mathf.PI) * arcHeight;

                Vector3 newPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + verticalOffset, horizontalPosition.z);
                transform.position = newPosition;

                Vector3 direction = (newPosition - previousPosition).normalized;

                if (direction != Vector3.zero)
                {
                    transform.rotation = Quaternion.LookRotation(direction);
                }
                previousPosition = newPosition;
            }
            else
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
                transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                if (transform.position.y <= 2f)
                {
                    Explode();
                }
            }
        }

        void Explode()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, damagesRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.tag == "Classic Enemy" || collider.tag == "destructible")
                {
                    Debug.Log("Zombie touché par l'explosion !");
                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    float damageMultiplier = CalculateDamageMultiplier(distance);
                    float finalDamage = damages * damageMultiplier;

                    Damage(collider.transform, finalDamage);
                }
            }
            GameObject audioSource = GetComponent<AudioSource>().gameObject;
            audioSource.GetComponent<AudioSource>().Play();
            GameObject effect = Instantiate(bulletImpact, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(effect, 2f);
            Destroy(gameObject);
        }

        float CalculateDamageMultiplier(float distance)
        {
            if (distance <= damagesRadius * 0.75f)
            {
                return 1f;
            }
            else if (distance <= damagesRadius)
            {
                float normalizedDistance = (distance - (damagesRadius * 0.75f)) / (damagesRadius * 0.75f);
                return Mathf.Lerp(1f, 0.1f, normalizedDistance);
            }

            return 0f;
        }

        public void SetAerialLaunch(bool _aerialLaunch)
        {
            aerialLaunch = _aerialLaunch;
        }

        public void SetSpeed(float _newSpeed)
        {
            speed = _newSpeed;
        }
    }
}
