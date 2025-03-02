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
        statsText.text = $"Vagues survécues : {PlayerStats.PassedWaves} \n" +
                         $"Argent dépensé : {PlayerStats.MoneySpent} \n" +
                         $"Zombies tués : {PlayerStats.NbKilledEmenies} \n" +
                         $"Dégats infligés : {PlayerStats.DamagesGiven} \n" +
                         $"Défenses construites : {PlayerStats.BuiltDefenses}";
    }
}
