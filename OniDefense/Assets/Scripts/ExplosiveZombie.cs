using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveZombie : EnemyBase
{
    public float explosionRadius = 5f;
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

        // Trouver les défenses proches
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("destructible"))
            {
                DefenseScript defense = collider.GetComponent<DefenseScript>();
                if (defense != null)
                {
                    defense.TakeDamage(explosionDamage);
                }
            }
        }
    }
}
