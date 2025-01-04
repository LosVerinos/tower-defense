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

    [SerializeField] public GameObject standartDefense;
    [SerializeField] public GameObject sniperDefense;
    private GameObject defenseToBuild;

    void Start(){
        defenseToBuild = null;
    }

    public GameObject GetDefenseToBuild(){
        return defenseToBuild;
    }

    public void SetDefenseToBuild(GameObject _defense){
        defenseToBuild = _defense;
    }

}
