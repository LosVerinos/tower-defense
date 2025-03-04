using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
class ZombieFactory : MonoBehaviour
{
    // Variables pour la difficulté croissante
    private static float enemyHealthMultiplier = 1.1f;
    private static float enemySpeedMultiplier = 1.05f;
    public Transform defaultSpawnPoint;
    public Transform defaultObjectivePoint;
    [SerializeField] public GameObject[] zombies;

    public void SpawnZombie(int waveNumber, int selectedZombie){

        GameObject spawnedZombie = Instantiate(zombies[selectedZombie], defaultSpawnPoint.position, defaultSpawnPoint.rotation);
        if (spawnedZombie.tag.CompareTo("Classic Enemy") == 0)
        {
            var navigationScript = spawnedZombie.GetComponent<AINavigationScript>();
            navigationScript.objectivePoint = defaultObjectivePoint;
            navigationScript.agent = spawnedZombie.GetComponent<UnityEngine.AI.NavMeshAgent>();
            navigationScript.agent.speed *= Mathf.Pow(enemySpeedMultiplier, waveNumber);
        }
        if (spawnedZombie.tag.CompareTo("Flying Enemy") == 0)
        {
            var flyingScript = spawnedZombie.GetComponent<FlyingEnemyNavigationScript>();
            flyingScript.target = defaultObjectivePoint;
            flyingScript.speed *= Mathf.Pow(enemySpeedMultiplier, waveNumber);
        }
        spawnedZombie.GetComponent<EnemyBase>().health *= Mathf.Pow(enemyHealthMultiplier, waveNumber);
    }

}