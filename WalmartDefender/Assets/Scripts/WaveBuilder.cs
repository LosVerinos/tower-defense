using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class WaveBuilder
    {
        private Wave wave;

        public WaveBuilder(int waveNumber)
        {
            wave = new Wave();
            wave.count = CalculateZombiesForWave(waveNumber);
        }

        public WaveBuilder DoubleWaveCount()
        {
            wave.count = wave.count * 2;
            return this;
        }

        public WaveBuilder AddZombieType(int type, float ratio)
        {
            wave.zombieRatios[type] = ratio;
            return this;
        }

        public WaveBuilder SetSpecialWave(string type)
        {
            wave.isSpecialWave = true;
            wave.specialWaveType = type;
            return this;
        }

        public WaveBuilder SetBossCount(int waveNumber)
        {
            if ((waveNumber + 1) % 15 == 0)
            {
                wave.bossCount = Mathf.RoundToInt((waveNumber + 1) / 20f * 1.5f);
            }
            return this;
        }

        public Wave Build()
        {
            return wave;
        }

        public int CalculateZombiesForWave(int waveNumber)
        {
            float baseZombies = 1.0f;
            float growthRate = 1.5f;
            float variability = 0.3f;

            float baseCount = baseZombies + growthRate * waveNumber;

            float variation = (float)(variability * waveNumber * (new System.Random().NextDouble() - 0.5));
            int zombieCount = (int)Math.Max(1, baseCount + variation);

            return zombieCount;
        }
    }

}
