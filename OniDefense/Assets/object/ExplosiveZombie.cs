using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ExplosiveZombie : Zombie
    {
        public float explosionRadius = 10f;
        public float explosionDamage = 2000f;
        public GameObject explosionEffect;


        new public int index = 4;

        public override void Die()
        {
            Explode();
            base.Die();
        }

    void Explode()
    {
        Debug.Log("Le boss explose à sa mort !");
        
        // Création de l'effet visuel
        GameObject effect = null;
        if (explosionEffect != null)
        {
            effect = Instantiate(explosionEffect, transform.position, Quaternion.Euler(-90, 0, 0));
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

        if(effect != null)
            Destroy(explosionEffect, 2f);
    }


        float CalculateDamageMultiplier(float distance){
            if (distance <= explosionRadius * 0.75f)
            {
                return 1f; 
            }
            else if (distance <= explosionRadius)
            {
                float normalizedDistance = (distance - (explosionRadius * 0.75f)) / (explosionRadius * 0.75f);
                return Mathf.Lerp(1f, 0.1f, normalizedDistance); 
            }
        
            return 0f;
        }
    }
}
