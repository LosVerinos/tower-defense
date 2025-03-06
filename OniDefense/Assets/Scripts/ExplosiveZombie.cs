using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ExplosiveZombie : EnemyBase
    {
        public float explosionRadius = 10f;
        public float explosionDamage = 2000f;
        public GameObject explosionEffect;

        protected override void Die()
        {
            Explode();
            base.Die();
        }

        void Explode()
        {
            Debug.Log("Le boss explose à sa mort !");

            // Création de l'effet visuel
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.Euler(-90, 0, 0));
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("destructible"))
                {
                    DefenseScript defense = collider.GetComponent<DefenseScript>();
                    if (defense != null)
                    {
                        float distance = Vector3.Distance(transform.position, defense.transform.position);
                        float damageMultiplier = CalculateDamageMultiplier(distance);
                        float finalDamage = explosionDamage * damageMultiplier;

                        defense.TakeDamage(finalDamage);
                    }
                }
            }
        }


        float CalculateDamageMultiplier(float distance)
        {
            if (distance <= explosionRadius * 0.5f)
            {
                return 1f;
            }
            else if (distance <= explosionRadius)
            {
                float normalizedDistance = (distance - (explosionRadius * 0.5f)) / (explosionRadius * 0.5f);
                return Mathf.Lerp(1f, 0.1f, normalizedDistance);
            }

            return 0f;
        }
    }

}

