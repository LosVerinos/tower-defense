using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public int count;
    public Dictionary<int, float> zombieRatios = new Dictionary<int, float>();
    public bool isSpecialWave = false;
    public string specialWaveType = "";
    public int bossCount = 0;
}