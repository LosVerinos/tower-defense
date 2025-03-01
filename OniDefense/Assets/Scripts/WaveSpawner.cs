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
            int groupSize = UnityEngine.Random.Range(1, currentWave.count/3);

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
        while(indexZombieSelected > currentWave.highestEnemy){
            indexZombieSelected = UnityEngine.Random.Range(0, GetComponent<ZombieFactory>().zombies.Length);
            if(indexZombieSelected == currentWave.highestEnemy && highestEnemyCount == 0)
            {
                break;
            }
        }
        if(indexZombieSelected == currentWave.highestEnemy){
            highestEnemyCount--;
        }

        GetComponent<ZombieFactory>().SpawnZombie(waveIndex, indexZombieSelected);
        EnemySpawned();
    }

    public static void EnemyDied(){
        EnemiesAliveCount--;
        if (EnemiesAliveCount <= 0)
        {
            Debug.Log("Wave cleared !");
            GameObject.FindObjectOfType<WaveSpawner>().playButton.interactable = true;
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

        newWave.count = (int)Mathf.Pow(difficultyMultiplier, generatedWaves.Count);
        newWave.highestEnemy = Mathf.Min(generatedWaves.Count / 3, GetComponent<ZombieFactory>().zombies.Length - 1);
        newWave.maxHighestEnemy = Mathf.Max(1, newWave.count / 5);
        newWave.rate = Mathf.Min(2.0f, 0.5f + generatedWaves.Count * 0.05f);
        generatedWaves.Add(newWave);
    }
}
