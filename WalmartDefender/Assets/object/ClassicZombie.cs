using System;
using UnityEngine;

namespace Game
{
    public class ClassicZombie : Zombie
    {
        new public int index = 0;

        new public MonoBehaviour navigation = new AINavigationScript();

        public override void Die()
        {
            base.Die();
        }
    }
}



