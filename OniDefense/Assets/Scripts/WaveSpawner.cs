using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAliveCount = 0;
    public Transform spawnPoint;
    public Transform alternativeSpawnPoint;
    private float altSpawnProbability = 0.3f;
    public float timeBetweenWaves = 1f;
    private List<Wave> generatedWaves = new List<Wave>();
    [DoNotSerialize] public static int waveIndex = 0;
    public float difficultyMultiplier = 1.3f;

    void Start(){
        ResetEnemiesAliveCount();
        GenerateNextWave();
    }

    void Update(){
        if(EnemiesAliveCount > 0 || !GameManager.isRunning){
            return;
        }
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        if (waveIndex - 1 >= generatedWaves.Count)
        {
            Debug.LogError("❌ Erreur : waveIndex dépasse la taille de generatedWaves ! Génération forcée.");
            GenerateNextWave(); // Génère une vague si elle n'existe pas encore
        }

        Wave currentWave = generatedWaves[waveIndex - 1];
        ResetEnemiesAliveCount();
        List<int> wavesZombies = new List<int>();
        foreach (var zombieType in currentWave.zombieRatios)
        {
            int zombieCount = Mathf.RoundToInt(currentWave.count * zombieType.Value);
            for (int i = 0; i < zombieCount; i++)
            {
                wavesZombies.Add(zombieType.Key);
            }
        }

        for (int i = 0; i < currentWave.bossCount; i++)
        {
            Transform spawnLocation = (waveIndex >= 20 && UnityEngine.Random.value < altSpawnProbability) 
            ? alternativeSpawnPoint : spawnPoint;

            GetComponent<ZombieFactory>().SpawnZombie(waveIndex, 4, spawnLocation);
            Debug.Log("Spawn d'un boss !");
            EnemySpawned();
            yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f));
        }

        while (wavesZombies.Count > 0)
        {
            int groupSize = UnityEngine.Random.Range(1, Mathf.Min(6, wavesZombies.Count));

            for (int i = 0; i < groupSize; i++)
            {
                int zombieType = wavesZombies[0];
                wavesZombies.RemoveAt(0);

                Transform spawnLocation = (waveIndex >= 20 && UnityEngine.Random.value < altSpawnProbability) 
                ? alternativeSpawnPoint : spawnPoint;

                GetComponent<ZombieFactory>().SpawnZombie(waveIndex, zombieType, spawnLocation);
                EnemySpawned();
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f, 0.6f));
        }
        GenerateNextWave();
    }

    public static void EnemyDied(){
        EnemiesAliveCount--;
        if (EnemiesAliveCount == 0)
        {
            Debug.Log("Wave cleared !");
            PlayerStats.PassedWaves++;
        }
    }

    public static void EnemySpawned(){
        EnemiesAliveCount++;
    }

    private static void ResetEnemiesAliveCount(){
        EnemiesAliveCount = 0;
    }

    private void GenerateNextWave(){
        Wave newWave = new Wave();
        newWave.count = CalculateZombiesForWave(waveIndex);

        float zombie1Ratio = 1.0f, zombie2Ratio = 0.0f, zombie3Ratio = 0.0f, zombie4Ratio = 0.0f;

        if (waveIndex+1 >= 5)
        {
            zombie1Ratio = 0.8f;
            zombie2Ratio = 0.2f;
        }
        if (waveIndex+1 >= 10)
        {
            zombie1Ratio = 0.6f;
            zombie2Ratio = 0.25f;
            zombie3Ratio = 0.15f;
        }
        if (waveIndex+1 >= 15)
        {
            zombie4Ratio = 0.1f;
        }

        if (UnityEngine.Random.value < 0.2f && waveIndex > 10)
        {
            newWave.isSpecialWave = true;
            int specialType = UnityEngine.Random.Range(0, 5);

            switch (specialType)
            {
                case 0: // Vague Sprint (100% zombies rapides)
                    newWave.specialWaveType = "Sprint";
                    newWave.zombieRatios.Add(1, 1.0f);
                    break;

                case 1: // Vague Tank (100% zombies résistants)
                    newWave.specialWaveType = "Tank";
                    newWave.zombieRatios.Add(2, 1.0f);
                    break;

                case 2: // Vague Tsunami (90% zombies classiques & 1,5x plus de zombie)
                    newWave.specialWaveType = "Tsunami";
                    newWave.count = (int)(newWave.count*1.5f);
                    newWave.zombieRatios.Add(0, 0.9f);
                    newWave.zombieRatios.Add(1, 0.1f);
                    break;


                case 3: // Vague Chaotique (répartition aléatoire)
                    newWave.specialWaveType = "Chaotique";
                    newWave.zombieRatios.Add(2, UnityEngine.Random.Range(0.1f, 0.3f));
                    newWave.zombieRatios.Add(0, UnityEngine.Random.Range(0.3f, 0.5f));
                    newWave.zombieRatios.Add(1, UnityEngine.Random.Range(0.2f, 0.4f));
                    newWave.zombieRatios.Add(3, UnityEngine.Random.Range(0.1f, 0.2f));
                    break;

                case 4: // "Volants"
                    newWave.specialWaveType = "Volants";
                    newWave.zombieRatios.Add(3, 1.0f); // 100% Corbeaux (pour l'instant)
                break;
            }

            Debug.Log("Vague spéciale détectée : " + newWave.specialWaveType);
        }
        else
        {
            newWave.zombieRatios.Add(0, zombie1Ratio);
            if (zombie4Ratio != 0) newWave.zombieRatios.Add(3, zombie4Ratio);
            if (zombie2Ratio != 0) newWave.zombieRatios.Add(1, zombie2Ratio);
            if (zombie3Ratio != 0) newWave.zombieRatios.Add(2, zombie3Ratio);
            
        }

        if ((waveIndex + 1) % 15 == 0)
        {
            newWave.bossCount = Mathf.RoundToInt((waveIndex + 1) / 20f * 1.5f);
            Debug.Log("Vague Boss !");
        }

        generatedWaves.Add(newWave);
        Debug.Log("New generated wave n°" + (waveIndex + 1) + " : " + newWave.count + " zombies.");
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

    private float GetZombieSpeed(int zombieIndex)
    {
        GameObject zombiePrefab = GetComponent<ZombieFactory>().zombies[zombieIndex];
        NavMeshAgent walking = zombiePrefab.GetComponent<NavMeshAgent>();
        FlyingEnemyNavigationScript flying = zombiePrefab.GetComponent<FlyingEnemyNavigationScript>();
        if(walking != null){
            return walking.speed;
        }
        else{
            return flying.speed;
        }
    }
}
