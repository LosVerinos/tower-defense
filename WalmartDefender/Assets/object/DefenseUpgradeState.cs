using System;
using UnityEngine;

namespace Gamex
{
    [System.Serializable]
    public class DefenseUpgradeState
    {
        public GameObject prefab;
        public float maximumRange;
        public float minimumRange;
        public float damages;
        public float fireRate;
        public int cost;
        public float health;
        public float turningSpeed;
       


        public DefenseUpgradeState()
        {
            
        }
    }
}

