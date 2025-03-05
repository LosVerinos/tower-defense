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
    private List<Wave> generatedWaves = new List<Wave>();
    [DoNotSerialize] public static int waveIndex = 0;

    void Start(){
        ResetWaveSpawner();
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
            Debug.LogError("Erreur : waveIndex dépasse la taille de generatedWaves ! Génération forcée.");
            GenerateNextWave();
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

    private void GenerateNextWave()
    {
        WaveFactory factory = (UnityEngine.Random.value < 0.2f && waveIndex > 10) 
            ? new SpecialWaveFactory() 
            : new StandardWaveFactory();

        Wave newWave = factory.CreateWave(waveIndex);

        if ((waveIndex + 1) % 15 == 0) // Ajout d'une vague Boss
        {
            newWave.bossCount = Mathf.RoundToInt((waveIndex + 1) / 20f * 1.5f);
        }

        generatedWaves.Add(newWave);
        Debug.Log($"Nouvelle vague n°{waveIndex + 1} - {newWave.specialWaveType}");
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

    private void ResetWaveSpawner(){
        ResetEnemiesAliveCount();
        waveIndex = 0;
        generatedWaves.Clear();
    }
}
