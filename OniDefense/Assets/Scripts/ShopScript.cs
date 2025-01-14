using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public DefenseClass standartDefense;
    public DefenseClass sniperDefense;
    public DefenseClass mortarDefense;
    public DefenseClass barricade;
    BuildManager buildManager;

    // Références aux boutons
    public Button shopButtonMitrailleuse;
    public Button shopButtonSniper;
    public Button shopButtonMortar;
    public Button shopButtonBarricade;

    public void SelectMitrailleuse()
    {
        if (PlayerStats.Money >= standartDefense.cost)
        {
            Debug.Log("Mitrailleuse sélectionnée");
            buildManager.SelectDefenseToBuild(standartDefense);
            EnableButton(shopButtonMitrailleuse);
            return;
        }
        Debug.Log("Not enough money!");
        buildManager.SelectDefenseToBuild(null);
    }

    public void SelectSniper()
    {
        if (PlayerStats.Money >= sniperDefense.cost)
        {
            Debug.Log("Sniper sélectionné");
            buildManager.SelectDefenseToBuild(sniperDefense);
            EnableButton(shopButtonSniper);
            return;
        }
        Debug.Log("Not enough money!");
        buildManager.SelectDefenseToBuild(null);
    }

        public void SelectMortar()
    {
        if (PlayerStats.Money >= mortarDefense.cost)
        {
            Debug.Log("Mortier sélectionné");
            buildManager.SelectDefenseToBuild(mortarDefense);
            EnableButton(shopButtonMortar);
            return;
        }
        Debug.Log("Not enough money!");
        buildManager.SelectDefenseToBuild(null);
    }

    public void SelectBarricade()
    {
        if (PlayerStats.Money >= barricade.cost)
        {
            Debug.Log("Barricade sélectionnée");
            buildManager.SelectDefenseToBuild(barricade);
            EnableButton(shopButtonBarricade);
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
        if (PlayerStats.Money < sniperDefense.cost)
        {
            DisableButton(shopButtonSniper);
        }
        else
            EnableButton(shopButtonSniper);

        if (PlayerStats.Money < standartDefense.cost)
        {
            DisableButton(shopButtonMitrailleuse);
        }
        else
            EnableButton(shopButtonMitrailleuse);
        if (PlayerStats.Money < mortarDefense.cost)
        {
            DisableButton(shopButtonMortar);
        }
        else
            EnableButton(shopButtonMortar);
        if (PlayerStats.Money < barricade.cost)
        {
            DisableButton(shopButtonBarricade);
        }
        else
            EnableButton(shopButtonBarricade);   
    }
}
