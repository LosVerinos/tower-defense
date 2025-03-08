using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game
{
    public abstract class Ennemy
    {
        private float baseHealth;
        private float health;
        private int reward;
        private Canvas canvas;
        private readonly Image healthBar;
        private NavMeshAgent agent;
        private enemyType type;
        private GameObject gameObject;

        public Ennemy()
        {

        }

        public void takeDammage(float amount)
        {
            health -= amount;
            canvas.enabled = true;
            healthBar.fillAmount = health / baseHealth;
            if (health <= 0)
                Die();
        }
        private void Die()
        {
            //Destroy(gameObject);
            PlayerStats.Money += reward * PlayerStats.moneyMultiplier;
        }

    }
}