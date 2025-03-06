using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public int startLives = 1;

        public static int moneyMultiplier = 1;
        public static float fireRateMultiplier = 1;

    public static int Lives;
    public static int PassedWaves = 0;
    public static int NbKilledEnemies = 0;
    public static int BuiltDefenses = 0;
    public static float DamagesGiven = 0f;
    public static int MoneySpent = 0;

    void Start()
    {
        ResetStats();
    }

    public static void ResetStats()
    {
        Money = 400;
        Lives = 20;
        PassedWaves = 0;
        NbKilledEnemies = 0;
        BuiltDefenses = 0;
        DamagesGiven = 0f;
        MoneySpent = 0;
    }

    public static void DecreaseLives(int _lives)
    {
        Lives -= _lives;
        if (Lives < 0) Lives = 0;
    }

    public static void IncreaseLives(int _lives)
    {
        Lives += _lives;
    }

    public static void DecreaseMoney(int _money)
    {
        Money -= _money;
        MoneySpent += _money;
        if (Money < 0) Money = 0;
    }

    public static void IncreaseMoney(int _money)
    {
        Money += _money;
    }

    public static void EnemyKilled()
    {
        NbKilledEnemies++;
    }

    public static void WaveCompleted()
    {
        PassedWaves++;
    }

    public static void DefenseBuilt()
    {
        BuiltDefenses++;
    }

    public static void AddDamage(float damage)
    {
        DamagesGiven += damage;
    }
}