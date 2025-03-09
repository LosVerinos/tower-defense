using Game;
using UnityEngine;

namespace Game
{
    public class FastZombie : Zombie
    {
        new public int index = 1;

        new public MonoBehaviour navigation = new AINavigationScript();

        public override void Die()
        {
            base.Die();
        }
    }

}
