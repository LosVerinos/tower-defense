using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class Defense
    {
        public string name;
        public int upgradeLevel;
        public List<DefenseUpgradeState> upgradeStates;
        public Transform target;
        public LayerMask ennemyLayer;
        public Transform movingPartx;
        public Transform movingParty;
        public bool active;

        public Defense()
        {

        }

        public void Shoot(Ennemy Target)
        {

        }
        public Transform findTarget()
        {
            return null;
        }

        public int GetSellAmount()
        {
            return upgradeStates[upgradeLevel].cost;
            
        }

    }

}

