using Game;
using UnityEngine;

namespace Game
{
    public class HeavyZombie : Zombie
    {
        new public int index = 2;

        new public MonoBehaviour navigation = new AINavigationScript();

        public override void Die()
        {
            base.Die();
        }
    }
}