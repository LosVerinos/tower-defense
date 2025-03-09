using UnityEngine;

namespace Game
{
    public abstract class Projectile : MonoBehaviour
    {
        protected Transform target;
        public float speed = 70f;
        public GameObject bulletImpact;
        protected float damages;

        // Méthode pour définir la cible
        public void Find(Transform _target)
        {
            target = _target;
        }

        // Méthode pour définir les dégâts
        public void SetDamage(float _damages)
        {
            damages = _damages;
        }

        // Méthode abstraite pour le comportement de mise à jour
        protected abstract void Update();

        // Méthode pour infliger des dégâts à une cible
        protected void Damage(Transform colliderTransform, float damagesTaken)
        {
            EnemyBase e = colliderTransform.GetComponent<EnemyBase>();
            if (e != null)
            {
                e.TakeDamages(damagesTaken);
                PlayerStats.DamagesGiven += damagesTaken;
            }
            DefenseScript defense = colliderTransform.GetComponent<DefenseScript>();
            if (defense != null)
            {
                defense.TakeDamage(damagesTaken);
            }
        }
    }
}
