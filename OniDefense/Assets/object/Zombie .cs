using System;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Game
{
    public class Zombie : Ennemy
    {
        private float baseHealt;
        private float health;
        private int reward;
        private Canvas canva;
        private readonly Image healthBar;
        private NavMeshAgent agent;
        private enemyType type;


        public Zombie()
        {

        }

        new public void takeDammage(float amount)
        {

        }
        private void Die()
        {

        }

    }
}