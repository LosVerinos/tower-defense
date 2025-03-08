using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Mitrailleuse : Defense
    {
        private string name;
        private int upgradeLevel;
        private List<DefenseUpgradeState> upgradeState;
        private Transform target;
        private LayerMask ennemyLayer;
        private Transform movingPartx;
        private Transform movingParty;
        private bool active;

        public Mitrailleuse()
        {

        }

        public new void Shoot(Ennemy Target)
        {

        }
        public new Transform findTarget()
        {
            return null;
        }

    }

}

