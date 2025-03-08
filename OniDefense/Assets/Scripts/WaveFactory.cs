using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class WaveFactory
    {
        public abstract Wave CreateWave(int waveNumber);
    }

}


