using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WaveManagerScript : MonoBehaviour
{
    public static int EnemiesAliveCount = 0;
    public Transform spawnPoint;
    public Transform objectivePoint;
    public float timeBetweenWaves = 10f;
    private float countdown = 5f;
    private int waveIndex = 0;
    private int nbEnemies;
    public GameObject[] zombiesList;
    [SerializeField] public Button playButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnWave(int waveNumber)
    {
        Debug.Log("Wave incoming");
        ResetEnemiesAliveCount();
        nbEnemies = waveNumber + 1;

        for (int i = 0; i < nbEnemies; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        GameObject randomZombie = zombiesList[UnityEngine.Random.Range(0, zombiesList.Length)];
        GameObject spawnedZombie = Instantiate(randomZombie, spawnPoint.position, spawnPoint.rotation);
        if (spawnedZombie.tag == "Classic Enemy")
            spawnedZombie.GetComponent<AINavigationScript>().objectivePoint = objectivePoint;
        if (spawnedZombie.tag == "Flying Enemy")
            spawnedZombie.GetComponent<FlyingEnemyNavigationScript>().target = objectivePoint;
        EnemySpawned();
    }

    public static void EnemyDied()
    {
        EnemiesAliveCount--;
        if (EnemiesAliveCount <= 0)
        {
            Debug.Log("Wave cleared !");
            GameObject.FindObjectOfType<WaveManagerScript>().playButton.interactable = true;
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


    public void OnButtonNextWave()
    {
        if (EnemiesAliveCount > 0)
            return;
        Debug.Log("Wave incoming");
        StartCoroutine(SpawnWave(waveIndex++));
        playButton.interactable = false;
    }
}
