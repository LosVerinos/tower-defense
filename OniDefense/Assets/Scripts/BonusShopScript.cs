using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusShopScript : MonoBehaviour
{

    public int surplusCost = 50;
    public int richZombieCost = 75;
    public float bonusDuration = 10f;

    public void BuySurplusAmmo()
    {
        if (PlayerStats.Money >= surplusCost)
        {
            PlayerStats.DecreaseMoney(surplusCost);
            StartCoroutine(ActivateSurplusAmmo());
        }
    }

    IEnumerator ActivateSurplusAmmo()
    {
        Debug.Log("Surplus de munitions activé");
        PlayerStats.fireRateMultiplier = 10f;
        yield return new WaitForSeconds(bonusDuration);
        PlayerStats.fireRateMultiplier = 1f;
    }

    public void BuyRichZombie()
    {
        if (PlayerStats.Money >= richZombieCost)
        {
            PlayerStats.DecreaseMoney(richZombieCost);
            StartCoroutine(ActivateRichZombie());
        }
    }

    IEnumerator ActivateRichZombie()
    {
        Debug.Log("Riches zombies activé");
        PlayerStats.moneyMultiplier = 5; // Double le gain d'argent
        yield return new WaitForSeconds(bonusDuration);
        PlayerStats.moneyMultiplier = 1;
    }

}
