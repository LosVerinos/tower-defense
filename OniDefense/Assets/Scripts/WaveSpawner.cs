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
        if(countdown <= 0){
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
            return;
        }

        countdown -= Time.deltaTime;
        countdown = Math.Clamp(countdown, 0f, Mathf.Infinity);
    }

    IEnumerator SpawnWave(){
        Wave currentWave = generatedWaves[waveIndex];
        highestEnemyCount = currentWave.maxHighestEnemy;
        int remainingEnemiesToSpawn = currentWave.count;
        Debug.Log("Wave incoming : " + currentWave.count + " Zs");
        ResetEnemiesAliveCount();
        while(remainingEnemiesToSpawn > 0){
            int groupSize = UnityEngine.Random.Range(2, Mathf.Min(6, currentWave.count / 5));

            for (int i = 0; i < groupSize; i++)
            {
                TrySpawnZombie(currentWave);
                remainingEnemiesToSpawn --;
            }
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.3f, 0.6f));
        }
        waveIndex++;
        GenerateNextWave();
    }

    private void TrySpawnZombie(Wave currentWave){
        int indexZombieSelected = 10;
        if (UnityEngine.Random.value > 0.7f && highestEnemyCount > 0) 
        {
            indexZombieSelected = currentWave.highestEnemy;
            highestEnemyCount--;
        }
        else
        {
            indexZombieSelected = UnityEngine.Random.Range(0, currentWave.highestEnemy + 1);
        }

        GetComponent<ZombieFactory>().SpawnZombie(waveIndex, indexZombieSelected);
        EnemySpawned();
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
        newWave.highestEnemy = Mathf.Min(generatedWaves.Count / 5, GetComponent<ZombieFactory>().zombies.Length - 1);
        newWave.maxHighestEnemy = Mathf.Clamp(newWave.count / 7, 1, newWave.count / 3);
        newWave.rate = Mathf.Min(2.0f, 0.5f + generatedWaves.Count * 0.05f);
        generatedWaves.Add(newWave);
    }

    public int CalculateZombiesForWave(int waveNumber)
    {
        // Paramètres ajustables
        float baseZombies = 1.0f;
        float growthRate = 1.5f;
        float variability = 0.3f;

        // Calcul du nombre de base de zombies pour la vague
        float baseCount = baseZombies + growthRate * waveNumber;

        // Ajout de la variabilité
        float variation = (float)(variability * waveNumber * (new System.Random().NextDouble() - 0.5));
        

        // Calcul du nombre final de zombies
        int zombieCount = (int)Math.Max(1, baseCount + variation);
        Debug.Log("New generated wave n°" + waveNumber + " : " + zombieCount + " zombies.");

        return zombieCount;
    }
}
