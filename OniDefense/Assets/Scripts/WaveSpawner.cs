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
    public float timeBetweenWaves = 5f;
    [SerializeField] public Wave[] waves;
    [DoNotSerialize] public int waveIndex = 0;
    private float countdown;
    [SerializeField] public Button playButton;
    private int currentDifficulty = 1;
    private int highestEnemyCount;
    public int baseEnemyCount = 1;
    public float difficultyMultiplier = 1.2f;

    void Start()
    {
        ResetEnemiesAliveCount();
        countdown = timeBetweenWaves;
    }

    void Update()
    {
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

    IEnumerator SpawnWave()
    {
        Wave currentWave = waves[waveIndex];
        highestEnemyCount = currentWave.maxHighestEnemy;
        Debug.Log("Wave incoming");
        ResetEnemiesAliveCount();

        CalculateCurrentDifficulty();
        
        int remainingDifficulty = currentDifficulty;
        

        while (remainingDifficulty >= 0)
        {
            TrySpawnZombie(ref remainingDifficulty, currentWave);
            yield return new WaitForSeconds(1 / currentWave.rate);
        }

        /*
        for(int i=0; i < currentWave.count; i++){
            TrySpawnZombie(ref remainingDifficulty);
            yield return new WaitForSeconds(1 / currentWave.rate);
        }*/

        waveIndex++;


        if(waveIndex == waves.Length){
            Debug.Log("Last wave of the level launched !");
        }

    }

    private void CalculateCurrentDifficulty()
    {
        currentDifficulty = (int)(baseEnemyCount * Math.Pow(difficultyMultiplier, waveIndex)) + waveIndex;
    }

    private void TrySpawnZombie(ref int remainingDifficulty, Wave currentWave)
    {
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
        remainingDifficulty -= (int)GetComponent<ZombieFactory>().zombies[indexZombieSelected].GetComponent<EnemyBase>().difficultyWeight;

        /*
        GameObject randomZombie = GetComponent<ZombieFactory>().CreateRandomZombieByDifficulty(remainingDifficulty, waveIndex);
        if (randomZombie != null)
        {
            remainingDifficulty -= (int)randomZombie.GetComponent<EnemyBase>().difficultyWeight;
            EnemySpawned();
        }
        */
    }

    private void SpawnZombie(){
        //TODO : SPAWN
    }

    public static void EnemyDied()
    {
        EnemiesAliveCount--;
        if (EnemiesAliveCount <= 0)
        {
            Debug.Log("Wave cleared !");
            GameObject.FindObjectOfType<WaveSpawner>().playButton.interactable = true;
        }
    }

    public static void EnemySpawned()
    {
        EnemiesAliveCount++;
    }

    private static void ResetEnemiesAliveCount()
    {
        EnemiesAliveCount = 0;
    }

/*
    public void OnButtonNextWave()
    {
        // LivesManager.Instance.Lives = 0;
        if (EnemiesAliveCount > 0)
            return;
        Debug.Log("Wave incoming");
        StartCoroutine(SpawnWave());
        playButton.interactable = false;
    }*/
}
