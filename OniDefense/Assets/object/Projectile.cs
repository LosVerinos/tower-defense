using System;
using UnityEngine;

namespace Game
{
    public abstract class Projectile
    {
        private GameObject prefab;
        private Ennemy ennemy;
        private float speed;
        private int damage;
        private GameObject bulletImpact;

        public Projectile()
        {
        }

        public void SetTarget(Ennemy target)
        {

        }

        public void SetDamage(float damage)
        {

        }
        public void HitTarget()
        {

        }
    }

}
