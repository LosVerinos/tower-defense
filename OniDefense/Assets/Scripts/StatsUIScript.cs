using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIScript : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    public void DisplayStats(){
        statsText.text = $"Vagues survécues: {PlayerStats.PassedWaves} \n" +
                         $"Argent: {PlayerStats.Money} \n" +
                         $"Vies: {PlayerStats.Lives.ToString()}";
    }
}
