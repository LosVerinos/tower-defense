using System;
using Game;

namespace Game
{
    public class CrowZombie : FlyingZombie
    {
        new public int index = 3;

        public override void Die()
        {
            base.Die();
        }

    }
}

