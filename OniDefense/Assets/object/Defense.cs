using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class Defense
    {
        private string name;
        private int upgradeLevel;
        private List<DefenseUpgradeState> upgradeState;
        private Transform target;
        private LayerMask ennemyLayer;
        private Transform movingPartx;
        private Transform movingParty;
        private bool active;

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

    }

}

