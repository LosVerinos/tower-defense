using System;
using UnityEngine;

namespace Gamex
{
    public class Node
    {
        private Defense defense;
        private Defense tempDefense;
        private Defense defenseClass;
        private Vector3 position;
        private BuildManager buildManager;
        private bool isUpgraded;

        public Node()
        {

        }
        public void onMouseEnter()
        {
            SetHover(true);
        }
        public void onMouseExit()
        {
            SetHover(false);
        }
        public void setOver(bool state)
        {
            if (IsPointerOverUIElement()) state = false;

            if (state)
            {
                rend.material = hoverMaterial;
                if (defense == null && buildManager.CanBuild)
                    buildManager.BuildDefenseOn(this, false, false);
            }
            else
            {
                rend.material = defaultMaterial;
                Destroy(tempDefense);
            }
        }
        public void onMouseDown()
        {
            if (IsPointerOverUIElement()){
            return;
            }

            if (defense != null)
            {
                buildManager.SelectNode(this);
                return;
            }

            if (!buildManager.CanBuild)
                return;

            Destroy(tempDefense);
            buildManager.BuildDefenseOn(this, true, false);
            DefenseScript defenseScript = defense.GetComponent<DefenseScript>();
            if(defenseScript != null)
                defenseScript.Initialize(buildManager.GetDefenseToBuild());
                    
            buildManager.SelectDefenseToBuild(null);
            tempDefense = null;
        }
        public void isPointerOverUIElement()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    //Debug.Log("Est pas sur de l'ui, TRUE");
                    return true; 
                }
            }
            
            return false;
        }
        public void upgradeDefense()
        {
            if (defenseClass.upgradeLevel+1 >= defenseClass.upgradeStates.Count - 1)
            {
                Debug.Log("La défense est au niveau max");
                return;
            }
            /*
            int upgradeCost = defenseClass.upgradeCosts[currentUpgradeState];
            if (PlayerStats.Money < upgradeCost)
            {
                Debug.Log("Pas assez d'argent pour améliorer!");
                return;
            

            PlayerStats.Money -= upgradeCost;}*/
            defenseClass.upgradeLevel++;

            DefenseUpgradeState newState = defenseClass.upgradeStates[defenseClass.upgradeLevel];

            if (defense != null)
                Destroy(defense);

            buildManager.SelectDefenseToBuild(defenseClass);
            buildManager.BuildDefenseOn(this, true, true);
            
            //defense = Instantiate(newState.prefab, transform.position + positionOffset, Quaternion.identity);
            //defense.transform.parent = transform;
            DefenseScript defenseScript = defense.GetComponent<DefenseScript>();
            if(defenseScript != null)
                defenseScript.Initialize(defenseClass);

            TurretScript turretScript = defense.GetComponent<TurretScript>();
            if (turretScript != null)
            {
                turretScript.damages = newState.damages;
                turretScript.maximumRange = newState.maximumRange;
                turretScript.fireRate = newState.fireRate;
                //turretScript.SetActive(true);
                turretScript.Initialize();
            }

            buildManager.SelectDefenseToBuild(null);
            tempDefense = null;

            isUpgraded = true;
            Debug.Log($"Defense améliorée au niveau {defenseClass.upgradeLevel}!");
        }
        public void sellDefense()
        {
            PlayerStats.Money += defenseClass.GetSellAmount();
            Destroy(defense);
            defenseClass = null;
        }
    }
}

