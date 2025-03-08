using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BulletScript : Projectile
    {
        protected override void Update()
        {
            if (target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z) - transform.position;
            float distanceTravelledThisFrame = speed * Time.deltaTime;

            if (direction.magnitude <= distanceTravelledThisFrame)
            {
                HitTarget();
                return;
            }

            transform.Translate(direction.normalized * distanceTravelledThisFrame, Space.World);
        }

        void HitTarget()
        {
            EnemyBase e = target.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.TakeDamages(damages);
                PlayerStats.DamagesGiven += damages;
            }
            GameObject effect = Instantiate(bulletImpact, new Vector3(e.transform.position.x, e.transform.position.y + 2.5f, e.transform.position.z), transform.rotation);
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
    }
}
