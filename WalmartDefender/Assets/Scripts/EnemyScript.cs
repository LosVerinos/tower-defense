using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game
{
    public class EnemyBase : MonoBehaviour
    {

        [SerializeField] public float baseHealth;
        [SerializeField] public int reward;
        [SerializeField] public int damage;
        [SerializeField] public float difficultyWeight;

        public MonoBehaviour navigation; 

        private bool isDead = false;
        public float health;
        public UnityEngine.UI.Image healthBar;
        private Canvas canvas;
        public NavMeshAgent agent;
        public int index;

            protected virtual void Start()
            {
                canvas = GetComponentInChildren<Canvas>();
                health = baseHealth;
            }

        // Update is called once per frame
        void Update()
        {

            }

        public virtual void TakeDamages(float damages)
        {
            health -= damages;
            canvas.enabled = true;
            healthBar.fillAmount = health / baseHealth;

            PlayerStats.AddDamage(damage);
            if (health <= 0)
                Die();
        }

        public virtual void Die()
        {
            if (isDead) return;
            isDead = true;
            PlayerStats.Money += reward * PlayerStats.moneyMultiplier;
            PlayerStats.EnemyKilled();
            //Debug.Log("Zombie tué ! +" + reward * PlayerStats.moneyMultiplier + "$ ! Monnaie actuelle : " + PlayerStats.Money);
            WaveSpawner.EnemyDied();
            Destroy(gameObject);
        }
    }
}
