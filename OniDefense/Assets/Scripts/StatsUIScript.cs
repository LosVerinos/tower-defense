using TMPro;
using UnityEngine;

public class StatsUIScript : MonoBehaviour
{
    public TextMeshProUGUI statsText;

    void Update()
    {
        DisplayStats();
    }

    public void DisplayStats()
    {
        statsText.text = $"Vagues survécues: {PlayerStats.PassedWaves}\n" +
                         $"Argent: {PlayerStats.Money}\n" +
                         $"Vies: {PlayerStats.Lives}\n" +
                         $"Ennemis tués: {PlayerStats.NbKilledEnemies}\n" +
                         $"Défenses construites: {PlayerStats.BuiltDefenses}\n" +
                         $"Dommages infligés: {PlayerStats.DamagesGiven:F1}\n" +
                         $"Argent dépensé: {PlayerStats.MoneySpent}";
    }
}