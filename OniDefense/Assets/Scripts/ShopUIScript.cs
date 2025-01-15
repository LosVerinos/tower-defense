using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopUIScript : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject effectPanel;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject effectButton;
    [SerializeField] private GameObject closeButton;

    private bool isPanelDeployed;
    private bool isShopDeployed;
    private bool isEffectDeployed;

    public void DeployShopPanel(){
        if(isShopDeployed)
            return;
        if(isEffectDeployed)
            ShrinkEffectPanel();
        shopPanel.transform.Translate(new Vector3(0,200,0));
        shopButton.transform.Translate(new Vector3(0,200,0));
        effectButton.transform.Translate(new Vector3(0,200,0));
        closeButton.transform.Translate(new Vector3(0,200,0));
        closeButton.SetActive(true);
        isShopDeployed = true;
    }

    public void DeployEffectPanel(){
        if(isEffectDeployed)
            return;
        if(isShopDeployed)
            ShrinkShopPanel();
        effectButton.transform.Translate(new Vector3(0,200,0));
        shopButton.transform.Translate(new Vector3(0,200,0));
        closeButton.transform.Translate(new Vector3(0,200,0));
        closeButton.SetActive(true);
        isEffectDeployed = true;        
        effectPanel.transform.Translate(new Vector3(0,200,0));
    }

    public void ShrinkShopPanel(){
        isShopDeployed = false;
        shopPanel.transform.Translate(new Vector3(0,-200,0));
        shopButton.transform.Translate(new Vector3(0,-200,0));
        effectButton.transform.Translate(new Vector3(0,-200,0));
        closeButton.transform.Translate(new Vector3(0,-200,0));
        closeButton.SetActive(false);
    }

    public void ShrinkEffectPanel(){
        isEffectDeployed = false;
        effectPanel.transform.Translate(new Vector3(0,-200,0));
        shopButton.transform.Translate(new Vector3(0,-200,0));
        effectButton.transform.Translate(new Vector3(0,-200,0));
        closeButton.transform.Translate(new Vector3(0,-200,0));
        closeButton.SetActive(false);
    }

    public void ShrinkAll(){
        if(isShopDeployed)
            ShrinkShopPanel();
        
        if(isEffectDeployed)
            ShrinkEffectPanel();
    }
}
