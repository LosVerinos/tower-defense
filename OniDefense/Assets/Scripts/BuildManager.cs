using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

namespace Game
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.Log("ERR: 2 buildManager");
                return;
            }
            instance = this;
        }

        private Defense defenseToBuild;
        private Node selectedNode;
        public SelectUIScript selectUI;
        public GameObject upgradeEffect;


        void Start()
        {
            defenseToBuild = null;
        }

        public BuildManager GetInstance()
        {
            if (instance == null)
            {
                return new BuildManager();
            }
            return instance;
        }

        public Defense GetDefenseToBuild()
        {
            return defenseToBuild;
        }

        public bool CanBuild { get { return defenseToBuild != null; } }

        public void SelectNode(Node node)
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

        public void SelectDefenseToBuild(Defense _defense)
        {
            defenseToBuild = _defense;
            selectedNode = null;

            selectUI.Hide();
        }

    public void BuildDefenseOn(Node node, bool activate, bool isUpgrade){
        //Debug.Log("Defense Level : " + defenseToBuild.upgradeLevel);
        int defenseLevel = 0;
        DefenseUpgradeState defenseState = null;
        if(isUpgrade){
            defenseLevel = node.defenseClass.upgradeLevel;
            //Debug.Log("Defense Level upgraded : " + defenseLevel);
            defenseState = node.defenseClass.upgradeStates[defenseLevel];
            GameObject effect = Instantiate(upgradeEffect, node.transform.position, node.transform.rotation);
            Destroy(effect, 2f);
        }
        else{
            defenseState = defenseToBuild.upgradeStates[defenseLevel];
        }


            if (PlayerStats.Money >= defenseState.cost || isUpgrade)
            {
                GameObject defense = Instantiate(defenseState.prefab, node.transform.position + node.positionOffset, Quaternion.identity);
                defense.transform.SetParent(node.transform);

                TurretScript defenseScript = defense.GetComponent<TurretScript>();
                NavMeshObstacle _navMeshObstacle = defense.GetComponent<NavMeshObstacle>();
                Effect effectScript = defense.GetComponent<Effect>();
                BoxCollider boxCollider = defense.GetComponent<BoxCollider>();

                //Stocke en tant que défense tmeporaire et désactivée
                if (!activate)
                {
                    node.tempDefense = defense;
                    if (_navMeshObstacle != null)
                        _navMeshObstacle.enabled = false;
                    return;
                }

                //Active la défense
                if (defenseScript != null)
                {
                    defenseScript.SetActive(activate);
                }

                //active l'effet s'il y en a un
                if (effectScript != null && activate)
                {
                    effectScript.ApplyEffect(node);
                }

                //Active la zone d'obstacle pour les zombies
                if (_navMeshObstacle != null)
                {
                    _navMeshObstacle.enabled = true;
                }

                //Active le box collider si la défense en a un (pour le barbelé pour l'instant)
                if (boxCollider != null && activate)
                {
                    boxCollider.enabled = true;
                }

            node.defense = defense;
            
            
            if (!isUpgrade) {
                node.defenseClass = new Defense();
                node.defenseClass.upgradeStates = defenseToBuild.upgradeStates;
                node.defenseClass.upgradeLevel = 0;
                node.defenseClass.name = defenseToBuild.name; //j'avais oublié ca, le nom était pas transféré
                PlayerStats.DefenseBuilt();
            }
            
            PlayerStats.DecreaseMoney(defenseState.cost);
            //Debug.Log("Defense construite. Monnaie restante : " + PlayerStats.Money);
        }

        }
    }

}
