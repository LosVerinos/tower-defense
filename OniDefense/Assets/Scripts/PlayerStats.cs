using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int moneyMultiplier = 1;
    public static float fireRateMultiplier = 1;


    void Start()
    {
        Money = startMoney;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
