using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerScript : MonoBehaviour
{

    public static int EnemiesAliveCount = 0;

    public GameObject normalZombie;
    public Transform spawnPoint;
    public Transform objectivePoint;
    public float timeBetweenWaves = 10f;
    private float countdown = 5f;
    private int waveIndex = 0;
    private int nbEnemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemiesAliveCount > 0)
        {
            return;
        }
        if(countdown < 0f){
            StartCoroutine(SpawnWave(waveIndex));
            countdown = timeBetweenWaves;
            waveIndex ++;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave(int waveNumber){
        Debug.Log("Wave incoming");
        ResetEnemiesAliveCount();
        nbEnemies = waveNumber+1;

        for(int i = 0; i < nbEnemies; i++){
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy(){
        Instantiate(normalZombie, spawnPoint.position, spawnPoint.rotation);
        normalZombie.GetComponent<AINavigationScript>().objectivePoint = objectivePoint;
        EnemySpawned();
    }
    
    public static void EnemyDied(){
        EnemiesAliveCount--;
    }
    
    public static void EnemySpawned(){
        EnemiesAliveCount++;
    }
    
    private static void ResetEnemiesAliveCount(){
        EnemiesAliveCount = 0;
    }
}
