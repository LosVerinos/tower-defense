using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{
    public class SelectUIScript : MonoBehaviour
    {
        private Node target;
        public bool isDeployed = false;
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
            if (_target.defenseClass != null)
            {
                float maxRange = _target.defenseClass.upgradeStates[_target.defenseClass.upgradeLevel].maximumRange;
                float minRange = _target.defenseClass.upgradeStates[_target.defenseClass.upgradeLevel].prefab.GetComponent<TurretScript>().minimumRange;
                _target.GetComponent<RangeIndicator>().ShowRange(maxRange, minRange, _target.transform);
            }
        }

        public void Hide()
        {
            if(isDeployed){
                Debug.Log("Deselect node");
                ui.SetActive(false);
                if (target != null)
                {
                    target.GetComponent<RangeIndicator>().HideRange();
                }
                isDeployed = false;
            }
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
            isDeployed = true;
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
                PlayerStats.Money < target.defenseClass.upgradeStates[target.defenseClass.upgradeLevel + 1].cost || 
                target.defenseClass.upgradeLevel+1 == target.defenseClass.upgradeStates.Count)

                upgradeButton.interactable = false;
            else
                upgradeButton.interactable = true;
        }

    }
}


