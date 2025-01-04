using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    BuildManager buildManager;
    public void PurschaseMitrailleuse(){
        Debug.Log("Mitrailleuse achetée");
        buildManager.SetDefenseToBuild(buildManager.standartDefense);        
    }

    public void PurschaseSniper(){
        Debug.Log("Sniper achetée");
        buildManager.SetDefenseToBuild(buildManager.sniperDefense);
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
