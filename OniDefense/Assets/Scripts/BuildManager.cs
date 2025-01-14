using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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

    private DefenseClass defenseToBuild;

    void Start(){
        defenseToBuild = null;
    }

    public GameObject GetDefenseToBuild(){
        return defenseToBuild.prefab;
    }

    public bool CanBuild{ get {return defenseToBuild != null; } }

    public void SelectDefenseToBuild(DefenseClass _defense){
        defenseToBuild = _defense;
    }

    public void BuildDefenseOn(NodeScript node, bool activate){
        if(PlayerStats.Money >= defenseToBuild.cost){
            GameObject defense = Instantiate(defenseToBuild.prefab, node.transform.position + node.positionOffset, Quaternion.identity);
            TurretScript defenseScript = defense.GetComponent<TurretScript>();
            defenseScript.SetActive(activate);

            if(!activate){
                node.tempDefense = defense;
                return;
            }
            node.defense = defense;
            PlayerStats.Money -= defenseToBuild.cost;
            Debug.Log("Defense construite. Monnaie restante : " + PlayerStats.Money);
        }

    }

}
