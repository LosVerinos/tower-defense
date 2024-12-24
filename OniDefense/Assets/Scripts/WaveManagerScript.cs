using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerScript : MonoBehaviour
{

    public GameObject normalZombie;
    public Transform spawnPoint;
    public Transform objectivePoint;
    public float timeBetweenWaves = 10f;
    private float countdown = 5f;
    private int waveNumber = 1;
    private int nbEnemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown < 0f){
            SpawnWave(waveNumber);
            countdown = timeBetweenWaves;
            waveNumber ++;
        }

        countdown -= Time.deltaTime;
    }

    void SpawnWave(int waveNumber){
        Debug.Log("Wave incoming");
        nbEnemies = waveNumber*waveNumber+1;

        for(int i = 0; i < nbEnemies; i++){
            SpawnEnemy();
        }
    }

    void SpawnEnemy(){
        Instantiate(normalZombie, spawnPoint.position, spawnPoint.rotation);
        normalZombie.GetComponent<AINavigationScript>().objectivePoint = objectivePoint;
    }
}
