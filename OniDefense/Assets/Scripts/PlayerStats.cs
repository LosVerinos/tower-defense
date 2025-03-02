using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;
    public int startLives = 20;

    public static int moneyMultiplier = 1;
    public static float fireRateMultiplier = 1;

    public static int Lives;
    public static int PassedWaves;
    public static int NbKilledEmenies;
    public static int BuiltDefenses;
    public static float DamagesGiven;
    public static int MoneySpent;
    void Start()
    {
        Money = startMoney;  
        Lives = startLives; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void DecreaseLives(int _lives){
        Lives -= _lives;
    }

    public static void IncreaseLives(int _lives){
        Lives += _lives;
    }

    public static void DecreaseMoney(int _money){
        Money -= _money;
        MoneySpent += _money;
    }

    public static void IncreaseMoney(int _money){
        Money += _money;
    }
}
