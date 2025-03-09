using System;
using System.Collections.Generic;

namespace Game
{

    public interface AttackStrategy
    {
        void ExecuteAttack(List<Ennemy> ennemies) { }
    }
}



