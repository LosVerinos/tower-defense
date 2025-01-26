using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DefenseUpgradeState
{
    public GameObject prefab;    
    public float damages;        
    public float maximumRange;   
    public float fireRate;      
}

[System.Serializable]
public class DefenseClass
{
    public GameObject prefab;                      
    public int cost;                               
    public List<DefenseUpgradeState> upgradeStates; 
    public int[] upgradeCosts;                    
}
