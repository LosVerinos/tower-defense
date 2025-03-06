using TMPro;
using UnityEngine;

public class StatsUIScript : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    public void DisplayStats(){
        statsText.text = $"Vagues survécues : {PlayerStats.PassedWaves} \n" +
                         $"Argent dépensé : {PlayerStats.MoneySpent} \n" +
                         $"Zombies tués : {PlayerStats.NbKilledEnemies} \n" +
                         $"Dégats infligés : {PlayerStats.DamagesGiven} \n" +
                         $"Défenses construites : {PlayerStats.BuiltDefenses}";
    }
}