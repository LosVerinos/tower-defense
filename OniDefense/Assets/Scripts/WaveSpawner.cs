using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int EnemiesAliveCount = 0;
    public Transform spawnPoint;
    public Transform objectivePoint;
    public float timeBetweenWaves = 1f;
    private int baseEnemyCount = 1;
    private List<Wave> generatedWaves = new List<Wave>();
    [DoNotSerialize] public int waveIndex = 0;
    private float countdown;
    [SerializeField] public Button playButton;
    private int highestEnemyCount;
    public float difficultyMultiplier = 1.3f;

    void Start(){
        ResetEnemiesAliveCount();
        countdown = timeBetweenWaves;
        for (int i = 0; i < 3; i++)
        {
            GenerateNextWave();
        }
    }

    void Update(){
        if(EnemiesAliveCount > 0 || !GameManager.isRunning){
            return;
        }
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        Wave currentWave = generatedWaves[waveIndex];
        ResetEnemiesAliveCount();
        List<int> zombieSpawnOrder = new List<int>();
        foreach (var zombieType in currentWave.zombieRatios)
        {
            int zombieCount = Mathf.RoundToInt(currentWave.count * zombieType.Value);
            for (int i = 0; i < zombieCount; i++)
            {
                zombieSpawnOrder.Add(zombieType.Key);
            }
        }

        zombieSpawnOrder.Sort((z1, z2) => GetZombieSpeed(z1).CompareTo(GetZombieSpeed(z2)));

        while (zombieSpawnOrder.Count > 0)
        {
            int groupSize = UnityEngine.Random.Range(1, Mathf.Min(6, zombieSpawnOrder.Count));

            for (int i = 0; i < groupSize; i++)
            {
                int zombieType = zombieSpawnOrder[0];
                zombieSpawnOrder.RemoveAt(0);
                GetComponent<ZombieFactory>().SpawnZombie(waveIndex, zombieType);
                EnemySpawned();
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f, 0.6f));
        }

        waveIndex++;
        GenerateNextWave();
    }

    public static void EnemyDied(){
        EnemiesAliveCount--;
        if (EnemiesAliveCount <= 0)
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
        newWave.count = CalculateZombiesForWave(generatedWaves.Count);

        float zombie1Ratio = 1.0f, zombie2Ratio = 0.0f, zombie3Ratio = 0.0f;

        if (generatedWaves.Count >= 5)
        {
            zombie1Ratio = 0.8f;
            zombie2Ratio = 0.2f;
        }
        if (generatedWaves.Count >= 10)
        {
            zombie1Ratio = 0.6f;
            zombie2Ratio = 0.25f;
            zombie3Ratio = 0.15f;
        }

        if (UnityEngine.Random.value < 0.1f)//10% de chance de vague spéciale
        {
            newWave.isSpecialWave = true;
            int specialType = UnityEngine.Random.Range(0, 4);

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
                    newWave.zombieRatios.Add(0, UnityEngine.Random.Range(0.3f, 0.5f));
                    newWave.zombieRatios.Add(1, UnityEngine.Random.Range(0.2f, 0.4f));
                    newWave.zombieRatios.Add(2, UnityEngine.Random.Range(0.1f, 0.3f));
                    break;
            }

            Debug.Log("Vague spéciale détectée : " + newWave.specialWaveType);
        }
        else
        {
            newWave.zombieRatios.Add(0, zombie1Ratio);
            if (generatedWaves.Count >= 5) newWave.zombieRatios.Add(1, zombie2Ratio);
            if (generatedWaves.Count >= 10) newWave.zombieRatios.Add(2, zombie3Ratio);
        }

        generatedWaves.Add(newWave);
    }

    public int CalculateZombiesForWave(int waveNumber)
    {
        float baseZombies = 1.0f;
        float growthRate = 1.5f;
        float variability = 0.3f;

        float baseCount = baseZombies + growthRate * waveNumber;

        float variation = (float)(variability * waveNumber * (new System.Random().NextDouble() - 0.5));
        int zombieCount = (int)Math.Max(1, baseCount + variation);
        Debug.Log("New generated wave n°" + waveNumber + " : " + zombieCount + " zombies.");

        return zombieCount;
    }

    private float GetZombieSpeed(int zombieIndex)
    {
        GameObject zombiePrefab = GetComponent<ZombieFactory>().zombies[zombieIndex];
        return zombiePrefab.GetComponent<AINavigationScript>().speed; // Assurez-vous que vos zombies ont une variable "speed"
    }
}
