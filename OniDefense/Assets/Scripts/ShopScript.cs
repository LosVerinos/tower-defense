using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public DefenseClass standartDefense;
    public DefenseClass sniperDefense;
    BuildManager buildManager;
    public void SelectMitrailleuse(){
        if(PlayerStats.Money >= standartDefense.cost){
            Debug.Log("Mitrailleuse selectionné");
            buildManager.SelectDefenseToBuild(standartDefense);
        }
        else{
            Debug.Log("Not enough money !");
            //TODO: Afficher à l'ecran
            buildManager.SelectDefenseToBuild(null);
            return ;
        }
  
    }

    public void SelectSniper(){
        if(PlayerStats.Money >= sniperDefense.cost){
            Debug.Log("Sniper selectionné");
            buildManager.SelectDefenseToBuild(sniperDefense);
        }
        else{
            Debug.Log("Not enough money !");
            buildManager.SelectDefenseToBuild(null);
            //TODO: Afficher à l'ecran
            return ;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
