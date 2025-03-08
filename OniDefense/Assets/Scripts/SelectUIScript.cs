using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class SelectUIScript : MonoBehaviour
    {
        private Node target;

        public GameObject ui;
        public TextMeshProUGUI defenseNameText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI damageText;
        public TextMeshProUGUI rangeText;
        public TextMeshProUGUI fireRateText;
        public Button upgradeButton;
        public Button sellButton;
        public GameObject generalUI;

        public void SetTarget(Node _target)
        {
            target = _target;
            Display();
            UpdateUI();
        }

        public void Hide()
        {
            Debug.Log("Deselect node");
            ui.SetActive(false);
        }

        public void Upgrade()
        {
            target.UpgradeDefense();
            UpdateUI();
        }

        public void Sell()
        {
            target.SellDefense();
            BuildManager.instance.DeselectNode();
        }

        public void Display()
        {
            ui.SetActive(true);
            generalUI.GetComponent<ShopUIScript>().ShrinkAll();
        }

        private void UpdateUI()
        {
            if (target == null || target.defenseClass == null) return;

            DefenseUpgradeState state = target.defenseClass.upgradeStates[target.defenseClass.upgradeLevel];

            defenseNameText.text = "Nom : " + target.defenseClass.name;
            Debug.Log(target.defenseClass.name);
            levelText.text = "Niveau : " + (target.defenseClass.upgradeLevel + 1);
            damageText.text = "Dégâts : " + state.damages;
            rangeText.text = "Portée : " + state.maximumRange;
            fireRateText.text = "Cadence : " + state.fireRate;
            sellButton.GetComponentInChildren<TextMeshProUGUI>().text = "Vendre (" + target.defenseClass.GetSellAmount() + " $)";

            if (target.defenseClass.upgradeLevel + 1 < target.defenseClass.upgradeStates.Count)
            {
                int nextUpgradeCost = target.defenseClass.upgradeStates[target.defenseClass.upgradeLevel + 1].cost;
                upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Améliorer (" + nextUpgradeCost + " $)";
                upgradeButton.interactable = PlayerStats.Money >= nextUpgradeCost;
            }
            else
            {
                upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Niveau maximum";
                upgradeButton.interactable = false;
            }

            CheckButtonActivation();
        }


        private void CheckButtonActivation(){
            Debug.Log("Level : " + target.defenseClass.upgradeLevel+1 + ", count : " + target.defenseClass.upgradeStates.Count);
            if (
                //PlayerStats.Money < target.defenseClass.upgradeStates[target.defenseClass.upgradeLevel + 1].cost || 
                target.defenseClass.upgradeLevel+1 == target.defenseClass.upgradeStates.Count)

                upgradeButton.interactable = false;
            else
                upgradeButton.interactable = true;
        }
    }
}


