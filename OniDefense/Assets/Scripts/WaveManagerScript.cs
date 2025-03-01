using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManagerScript : MonoBehaviour
{
    public static int EnemiesAliveCount = 0;
    public Transform spawnPoint;
    public Transform objectivePoint;
    public float timeBetweenWaves = 10f;
    public int waveIndex = 0;
    public GameObject[] zombiesList;
    [SerializeField] public Button playButton;

    // Variables pour la difficulté croissante
    public int baseEnemyCount = 1;
    public int maxEnemiesPerWave = 20;
    public float enemyHealthMultiplier = 1.1f;
    public float enemySpeedMultiplier = 1.05f;
    public float difficultyMultiplier = 1.2f;
    private int currentDifficulty = 1;
    private GameObject gameOverPanel;

    void Start()
    {
        ResetEnemiesAliveCount();
        gameOverPanel = GameObject.FindGameObjectWithTag("GOPanel");
        gameOverPanel.SetActive(false);
    }

    void Update()
    {

    }

    IEnumerator SpawnWave()
    {
        Debug.Log("Wave incoming");
        ResetEnemiesAliveCount();
        CalculateCurrentDifficulty();
        Debug.Log($"Current Difficulty : {currentDifficulty}");

        int remainingDifficulty = currentDifficulty;

        while (remainingDifficulty >= 0)
        {
            GameObject randomZombie = TrySpawnZombie(ref remainingDifficulty);
            if (randomZombie == null)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        waveIndex++;
    }

    private void CalculateCurrentDifficulty()
    {
        //currentDifficulty = (int)(baseEnemyCount * Math.Pow(difficultyMultiplier, waveIndex)) + waveIndex;
        currentDifficulty = 50;
    }

    private GameObject TrySpawnZombie(ref int remainingDifficulty)
    {
        Debug.Log($"Spawning a zombie under diff : {remainingDifficulty}");
        GameObject randomZombie = GetComponent<ZombieFactory>().CreateRandomZombieByDifficulty(remainingDifficulty, waveIndex);
        if (randomZombie != null)
        {
            remainingDifficulty -= (int)randomZombie.GetComponent<EnemyBase>().difficultyWeight;
            Debug.Log("Spawning enemy with difficulty " + randomZombie.GetComponent<EnemyBase>().difficultyWeight);
            EnemySpawned();
        }
        return randomZombie;
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
        // LivesManager.Instance.Lives = 0;
        if (EnemiesAliveCount > 0)
            return;
        Debug.Log("Wave incoming");
        StartCoroutine(SpawnWave());
        playButton.interactable = false;
    }
}
