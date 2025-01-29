using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public DefenseClass object1;
    public DefenseClass object2;
    public DefenseClass object3;
    public DefenseClass object4;
    BuildManager buildManager;

    // Références aux boutons
    public Button shopButtonObject1;
    public Button shopButtonObject2;
    public Button shopButtonObject3;
    public Button shopButtonObject4;

    public void SelectObject1()
    {
        if (PlayerStats.Money >= object1.upgradeStates[0].cost)
        {
            buildManager.SelectDefenseToBuild(object1);
            EnableButton(shopButtonObject1);
            return;
        }
        Debug.Log("Not enough money!");
        buildManager.SelectDefenseToBuild(null);
    }

    public void SelectObject2()
    {
        if (PlayerStats.Money >= object2.upgradeStates[0].cost)
        {
            buildManager.SelectDefenseToBuild(object2);
            EnableButton(shopButtonObject2);
            return;
        }
        Debug.Log("Not enough money!");
        buildManager.SelectDefenseToBuild(null);
    }

    public void SelectObject3()
    {
        if (PlayerStats.Money >= object3.upgradeStates[0].cost)
        {
            buildManager.SelectDefenseToBuild(object3);
            EnableButton(shopButtonObject3);
            return;
        }
        Debug.Log("Not enough money!");
        buildManager.SelectDefenseToBuild(null);
    }

    public void SelectObject4()
    {
        if (PlayerStats.Money >= object4.upgradeStates[0].cost)
        {
            buildManager.SelectDefenseToBuild(object4);
            EnableButton(shopButtonObject4);
            return;
        }
        Debug.Log("Not enough money!");
        buildManager.SelectDefenseToBuild(null);
    }

    private void DisableButton(Button button)
    {
        if (button != null)
        {
            button.interactable = false;
        }
    }

    private void EnableButton(Button button)
    {
        if (button != null)
        {
            button.interactable = true;
        }
    }

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    void Update(){
        if (object1.upgradeStates != null && object1.upgradeStates.Count > 0 && PlayerStats.Money < object1.upgradeStates[0].cost)
        {
            DisableButton(shopButtonObject1);
        }
        else
            EnableButton(shopButtonObject1);

        if (object2.upgradeStates != null && object2.upgradeStates.Count > 0 && PlayerStats.Money < object2.upgradeStates[0].cost)
        {
            DisableButton(shopButtonObject2);
        }
        else
            EnableButton(shopButtonObject2);


        if (object3.upgradeStates != null && object3.upgradeStates.Count > 0 && PlayerStats.Money < object3.upgradeStates[0].cost)
        {
            DisableButton(shopButtonObject3);
        }
        else
            EnableButton(shopButtonObject3);
        
        if (object4.upgradeStates != null && object4.upgradeStates.Count > 0 && PlayerStats.Money < object4.upgradeStates[0].cost)
        {
            DisableButton(shopButtonObject4);
        }
        else
            EnableButton(shopButtonObject4);   
    }
}
