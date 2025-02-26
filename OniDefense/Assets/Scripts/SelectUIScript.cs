using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectUIScript : MonoBehaviour
{
    private NodeScript target;

    public GameObject ui;

    // Références UI
    public TextMeshProUGUI defenseNameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI fireRateText;
    public Button upgradeButton;
    public Button sellButton;

    public void SetTarget(NodeScript _target)
    {
        target = _target;
        UpdateUI();
        Display();
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
            upgradeButton.interactable = (PlayerStats.Money >= nextUpgradeCost);
        }
        else
        {
            upgradeButton.GetComponentInChildren<TextMeshProUGUI>().text = "Niveau maximum";
            upgradeButton.interactable = false;
        }
    }

    public void Display()
    {
        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeDefense();
        UpdateUI(); // Met à jour les infos après amélioration
    }

    public void Sell()
    {
        target.SellDefense();
        BuildManager.instance.DeselectNode();
    }
}
