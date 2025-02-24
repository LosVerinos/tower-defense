using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class WaveManagerScript : MonoBehaviour
{
    public static int EnemiesAliveCount = 0;
    public Transform spawnPoint;
    public Transform objectivePoint;
    public float timeBetweenWaves = 10f;
    private float countdown = 5f;
    public int waveIndex = 0;
    public GameObject[] zombiesList;
    [SerializeField] public Button playButton;

    // Variables pour la difficulté croissante
    public int baseEnemyCount = 1;
    public int maxEnemiesPerWave = 20;
    public float enemyHealthMultiplier = 1.1f;
    public float enemySpeedMultiplier = 1.05f;
    public float difficultyMultiplier = 1.2f;
    private float currentDifficulty = 1f;
    private GameObject gameOverPanel;

    void Start()
    {
        ResetEnemiesAliveCount();
        gameOverPanel = GameObject.FindGameObjectWithTag("GOPanel");
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (LivesManager.Instance.Lives <= 0 && gameOverPanel.activeSelf == false)
        {
            gameOverPanel.SetActive(true);
        }
    }

    IEnumerator SpawnWave(int waveNumber)
    {
        Debug.Log("Wave incoming");
        ResetEnemiesAliveCount();
        currentDifficulty = baseEnemyCount * Mathf.Pow(difficultyMultiplier, waveNumber);

        float remainingDifficulty = currentDifficulty;
        bool enemySpawned = false;

        while (remainingDifficulty > 0)
        {
            GameObject randomZombie = zombiesList[UnityEngine.Random.Range(0, zombiesList.Length)];
            float difficultyWeight = randomZombie.GetComponent<EnemyBase>().difficultyWeight;

            if (remainingDifficulty >= difficultyWeight)
            {
                SpawnEnemy(randomZombie, waveNumber);
                remainingDifficulty -= difficultyWeight;
                enemySpawned = true;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                break;
            }
        }

        if (!enemySpawned)
        {
            GameObject fallbackZombie = zombiesList[0]; // Ensure at least one enemy is spawned
            SpawnEnemy(fallbackZombie, waveNumber);
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab, int waveNumber)
    {
        GameObject spawnedZombie = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        if (spawnedZombie.tag == "Classic Enemy")
        {
            var navigationScript = spawnedZombie.GetComponent<AINavigationScript>();
            navigationScript.objectivePoint = objectivePoint;
            navigationScript.agent = spawnedZombie.GetComponent<UnityEngine.AI.NavMeshAgent>();
            navigationScript.agent.speed *= Mathf.Pow(enemySpeedMultiplier, waveNumber);
        }
        if (spawnedZombie.tag == "Flying Enemy")
        {
            var flyingScript = spawnedZombie.GetComponent<FlyingEnemyNavigationScript>();
            flyingScript.target = objectivePoint;
            flyingScript.speed *= Mathf.Pow(enemySpeedMultiplier, waveNumber);
        }
        spawnedZombie.GetComponent<EnemyBase>().health *= Mathf.Pow(enemyHealthMultiplier, waveNumber);

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
        LivesManager.Instance.Lives = 0;
        // if (EnemiesAliveCount > 0)
        //     return;
        // Debug.Log("Wave incoming");
        // StartCoroutine(SpawnWave(waveIndex));
        // playButton.interactable = false;
    }
}
