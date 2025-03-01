using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

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
    private NodeScript selectedNode;

    public SelectUIScript selectUI;

    void Start(){
        defenseToBuild = null;
    }

    public DefenseClass GetDefenseToBuild(){
        return defenseToBuild;
    }

    public bool CanBuild{ get {return defenseToBuild != null; } }

    public void SelectNode(NodeScript node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }
        selectedNode = node;
        Debug.Log("Defense sélectionnée ");
        defenseToBuild = null;

        selectUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        selectUI.Hide();
    }

    public void SelectDefenseToBuild(DefenseClass _defense){
        defenseToBuild = _defense;
        selectedNode = null;

        selectUI.Hide();
    }

    public void BuildDefenseOn(NodeScript node, bool activate, bool isUpgrade){
        Debug.Log("Defense Level : " + defenseToBuild.upgradeLevel);
        int defenseLevel = 0;
        DefenseUpgradeState defenseState = null;
        if(isUpgrade){
            defenseLevel = node.defenseClass.upgradeLevel;
            Debug.Log("Defense Level upgraded : " + defenseLevel);
            defenseState = node.defenseClass.upgradeStates[defenseLevel];
        }
        else{
            defenseState = defenseToBuild.upgradeStates[defenseLevel];
        }


        if(PlayerStats.Money >= defenseState.cost || isUpgrade){
            GameObject defense = Instantiate(defenseState.prefab, node.transform.position + node.positionOffset, Quaternion.identity);
            defense.transform.SetParent(node.transform);

            TurretScript defenseScript = defense.GetComponent<TurretScript>();
            NavMeshObstacle _navMeshObstacle = defense.GetComponent<NavMeshObstacle>();
            Effect effectScript = defense.GetComponent<Effect>();
            BoxCollider boxCollider = defense.GetComponent<BoxCollider>();

            //Stocke en tant que défense tmeporaire et désactivée
            if(!activate){
                node.tempDefense = defense;
                if(_navMeshObstacle != null)
                    _navMeshObstacle.enabled = false;
                return;
            }

            //Active la défense
            if(defenseScript != null){
                defenseScript.SetActive(activate);
            }

            //active l'effet s'il y en a un
            if (effectScript != null && activate){
                effectScript.ApplyEffect(node);
            }

            //Active la zone d'obstacle pour les zombies
            if(_navMeshObstacle != null){
                _navMeshObstacle.enabled = true;
            }

            //Active le box collider si la défense en a un (pour le barbelé pour l'instant)
            if(boxCollider != null && activate){
                boxCollider.enabled = true;
            }

            node.defense = defense;
            
            
            if (!isUpgrade) {
                node.defenseClass = new DefenseClass();
                node.defenseClass.upgradeStates = defenseToBuild.upgradeStates;
                node.defenseClass.upgradeLevel = 0; 
            }
            
            PlayerStats.DecreaseMoney(defenseState.cost);
            //Debug.Log("Defense construite. Monnaie restante : " + PlayerStats.Money);
        }

    }
}
