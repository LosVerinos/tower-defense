using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    void Awake(){
        if(instance != null){
            Debug.Log("ERR: 2 buildManager");
            return;
        }
        instance = this;
    }

    public GameObject standartDefense;

    void Start(){
        defenseToBuild = standartDefense;
    }

    private GameObject defenseToBuild;

    public GameObject GetDefenseToBuild(){
        return defenseToBuild;
    }
}
