using System.Collections;
using System.Collections.Generic;
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
            
            PlayerStats.Money -= defenseToBuild.cost;
            Debug.Log("Defense construite. Monnaie restante : " + PlayerStats.Money);
        }

    }

}
