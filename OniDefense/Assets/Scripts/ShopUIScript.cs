using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIScript : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject effectPanel;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject effectButton;
    [SerializeField] private GameObject closeButton;
    private bool isShopDeployed;
    private bool isEffectDeployed;

    void Start(){
        closeButton.SetActive(false);
    }

    public void DeployShopPanel(){
        if(isShopDeployed)
            return;
        if(isEffectDeployed)
            ShrinkEffectPanel();

        GameObject[] panelsToMove = {effectButton, shopButton, shopPanel, closeButton};
        MovePanels(panelsToMove, new Vector3(0,230,0));

        closeButton.SetActive(true);
        isShopDeployed = true;
    }

    public void DeployEffectPanel(){
        if(isEffectDeployed)
            return;
        if(isShopDeployed)
            ShrinkShopPanel();

        GameObject[] panelsToMove = {effectButton, shopButton, effectPanel, closeButton};
        MovePanels(panelsToMove, new Vector3(0,230,0));

        closeButton.SetActive(true);
        isEffectDeployed = true;        
        
    }

    void ShrinkShopPanel(){
        isShopDeployed = false;
        GameObject[] panelsToMove = {shopPanel, shopButton, effectButton, closeButton};
        MovePanels(panelsToMove, new Vector3(0,-230,0));
        closeButton.SetActive(false);
    }

    void ShrinkEffectPanel(){
        isEffectDeployed = false;
        GameObject[] panelsToMove = {effectPanel, shopButton, effectButton, closeButton};
        MovePanels(panelsToMove, new Vector3(0,-230,0));
        closeButton.SetActive(false);
    }

    public void ShrinkAll(){
        if(isShopDeployed)
            ShrinkShopPanel();
        if(isEffectDeployed)
            ShrinkEffectPanel();
    }

    void MovePanel(GameObject panel, Vector3 movement){
        panel.transform.Translate(movement);
    }

    void MovePanels(GameObject[] panels, Vector3 movement){
        foreach(GameObject panel in panels){
            panel.transform.Translate(movement);
        }
        
    }
}
