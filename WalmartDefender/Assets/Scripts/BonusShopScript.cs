using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{

    public class BonusShopScript : MonoBehaviour
    {
        public Button buttonRichZombies;
        public Button buttonSurplus;
        public int surplusCost;
        public int richZombieCost;
        public float bonusDuration = 10f;

    void Update(){
        if (PlayerStats.Money < richZombieCost)
            buttonRichZombies.interactable = false;
        else
            buttonRichZombies.interactable = true;

        if (PlayerStats.Money < surplusCost)
            buttonSurplus.interactable = false;
        else
            buttonSurplus.interactable = true;
    }

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
}

