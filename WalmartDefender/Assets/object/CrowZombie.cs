using System;
using Game;
using UnityEngine;

namespace Game
{
    public class CrowZombie : FlyingZombie
    {
        new public int index = 3;

        new public MonoBehaviour navigation = new FlyingEnemyNavigationScript();

        public override void Die()
        {
            base.Die();
        }

    }
}

