using Game;

namespace Game
{
    public class HeavyZombie : Zombie
    {
        new public int index = 2;

        public override void Die()
        {
            base.Die();
        }
    }
}