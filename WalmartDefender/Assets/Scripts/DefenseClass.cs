using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class DefenseUpgradeState
    {
        public GameObject prefab;
        public float maximumRange;
        public float damages;
        public float fireRate;
        public int cost;
        public float health;
    }
}
