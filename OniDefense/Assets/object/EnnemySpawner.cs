using System;
using System.Collections.Generic;
using UnityEngine;


namespace Game
{

    public class EnnemySpawner
    {
        private Transform startPoint;
        private Transform objectivePoint;
        private float timeBetweenWaves;
        private float countDown;
        private int waveIndex;
        private int ennemyCount;
        private List<Ennemy> ennemyList;

        public EnnemySpawner()
        {
        }

        public void SpawnWave(int waveIndex)
        {

        }
        public Ennemy SpawnEnnemy()
        {
            return null;
        }
    }
}

