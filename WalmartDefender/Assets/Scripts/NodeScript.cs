using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Game
{
    public class Node : MonoBehaviour
    {

        private Renderer rend;
        public Material hoverMaterial;
        private Material defaultMaterial;
        [DoNotSerialize] public GameObject defense;
        [DoNotSerialize] public GameObject tempDefense;
        [DoNotSerialize] public Defense defenseClass;
        public Vector3 positionOffset;
        BuildManager buildManager;
        private GameObject maxRangeIndicator;
        private GameObject minRangeIndicator;
        private RangeIndicator rangeIndicator;
        public int currentUpgradeState = 0;
        public bool isUpgraded = false;

        void Start()
        {
            rend = GetComponent<Renderer>();
            defaultMaterial = rend.material;
            rangeIndicator = gameObject.AddComponent<RangeIndicator>();
            buildManager = BuildManager.instance;
        }

        void OnMouseEnter()
        {
            SetHover(true);
        }

        void OnMouseExit()
        {
            SetHover(false);
        }

        public void SetHover(bool state)
        {
            if (IsPointerOverUIElement()) state = false;

            if (state)
            {
                rend.material = hoverMaterial;
                if (defense == null && buildManager.CanBuild){
                    buildManager.BuildDefenseOn(this, false, false);
                    if(tempDefense.GetComponent<TurretScript>() != null)
                        rangeIndicator.ShowRange(tempDefense.GetComponent<TurretScript>().maximumRange, tempDefense.GetComponent<TurretScript>().minimumRange, gameObject.transform);
                }
                
            }
            else
            {
                if(rangeIndicator != null && tempDefense != null)
                    rangeIndicator.HideRange();

                rend.material = defaultMaterial;
                Destroy(tempDefense);
            }
        }

        public void OnMouseDown()
        {
            if (IsPointerOverUIElement())
            {
                return;
            }

            if (defense != null)
            {
                // Replace this with a node object
                buildManager.SelectNode(this);
                return;
            }

            if (!buildManager.CanBuild)
                return;
            if(rangeIndicator != null && tempDefense != null)
                        rangeIndicator.HideRange();
            Destroy(tempDefense);
            buildManager.BuildDefenseOn(this, true, false);
            PlayerStats.BuiltDefenses++;
            DefenseScript defenseScript = defense.GetComponent<DefenseScript>();
            if(defenseScript != null)
                defenseScript.Initialize(buildManager.GetDefenseToBuild());
                    
            buildManager.SelectDefenseToBuild(null);
            tempDefense = null;
        }

        bool IsPointerOverUIElement()
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

        public void UpgradeDefense()
        {
            if (defenseClass.upgradeLevel + 1 >= defenseClass.upgradeStates.Count)
            // - 1 supprimé car ne pouvait pas passer au dernier niveau)
            {
                Debug.Log("La défense est au niveau max");
                return;
            }
            defenseClass.upgradeLevel++;

            DefenseUpgradeState newState = defenseClass.upgradeStates[defenseClass.upgradeLevel];

            if (defense != null)
                Destroy(defense);

            buildManager.SelectDefenseToBuild(defenseClass);
            buildManager.BuildDefenseOn(this, true, true);

            DefenseScript defenseScript = defense.GetComponent<DefenseScript>();
            if (defenseScript != null)
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

        public void SellDefense()
        {
            PlayerStats.Money += defenseClass.GetSellAmount();

            Destroy(defense);
            defenseClass = null;
        }
    }

}