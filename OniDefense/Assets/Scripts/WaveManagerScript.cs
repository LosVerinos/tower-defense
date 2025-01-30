using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManagerScript : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform objectivePoint;
    public float timeBetweenWaves = 10f;
    private float countdown = 5f;
    private int waveIndex = 0;
    private int nbEnemies;
    public GameObject[] zombiesList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countdown < 0f){
            StartCoroutine(SpawnWave(waveIndex));
            countdown = timeBetweenWaves;
            waveIndex ++;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave(int waveNumber){
        Debug.Log("Wave incoming");
        nbEnemies = waveNumber+1;

        for(int i = 0; i < nbEnemies; i++){
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy(){
        GameObject randomZombie = zombiesList[UnityEngine.Random.Range(0, zombiesList.Length)];
        GameObject spawnedZombie = Instantiate(randomZombie, spawnPoint.position, spawnPoint.rotation);
        if(spawnedZombie.tag == "Classic Enemy")
            spawnedZombie.GetComponent<AINavigationScript>().objectivePoint = objectivePoint;
        if(spawnedZombie.tag == "Flying Enemy")
            spawnedZombie.GetComponent<FlyingEnemyNavigationScript>().target = objectivePoint;
    }
}
