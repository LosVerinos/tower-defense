using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class StandardWaveFactory : WaveFactory
    {
        public override Wave CreateWave(int waveNumber)
        {
            WaveBuilder builder = new WaveBuilder(waveNumber);

            float zombie1Ratio = 1.0f, zombie2Ratio = 0.0f, zombie3Ratio = 0.0f, zombie4Ratio = 0.0f;

            if (waveNumber + 1 >= 5) { zombie1Ratio = 0.8f; zombie2Ratio = 0.2f; }
            if (waveNumber + 1 >= 10) { zombie1Ratio = 0.6f; zombie2Ratio = 0.25f; zombie3Ratio = 0.15f; }
            if (waveNumber + 1 >= 15) { zombie4Ratio = 0.1f; }

            if (zombie3Ratio > 0) builder.AddZombieType(2, zombie3Ratio);
            builder.AddZombieType(0, zombie1Ratio);
            if (zombie2Ratio > 0) builder.AddZombieType(1, zombie2Ratio);
            if (zombie4Ratio > 0) builder.AddZombieType(3, zombie4Ratio);

            return builder.SetBossCount(waveNumber).Build();
        }

        private int CalculateZombiesForWave(int waveNumber)
        {
            float baseZombies = 1.0f;
            float growthRate = 1.5f;
            return Mathf.Max(1, Mathf.RoundToInt(baseZombies + growthRate * waveNumber));
        }
    }

}

