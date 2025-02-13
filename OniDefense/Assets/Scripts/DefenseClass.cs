using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class DefenseUpgradeState
{
    public GameObject prefab;    
    public float maximumRange;   
    public float damages; 
    public float fireRate;     
    public int cost;
    public float health;
}

[System.Serializable]
public class DefenseClass
{
    //public GameObject prefab;                    
    //public int cost;                               
    public List<DefenseUpgradeState> upgradeStates; 
    //public int[] upgradeCosts;
    [DoNotSerialize] public int upgradeLevel = 0;

    public int GetSellAmount()
    {
        return upgradeStates[upgradeLevel].cost / 2;
    }
}
