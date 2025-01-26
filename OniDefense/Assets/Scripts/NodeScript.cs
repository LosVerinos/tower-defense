using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeScript : MonoBehaviour
{
    private Renderer rend;
    public Material hoverMaterial;
    private Material defaultMaterial;
    [DoNotSerialize] public GameObject defense;
    [DoNotSerialize] public GameObject tempDefense;
    [DoNotSerialize] public DefenseClass defenseClass;
    public Vector3 positionOffset = new Vector3(0f, -0.3f, 0f);
    BuildManager buildManager;

    public int currentUpgradeState = 0; 
    public bool isUpgraded = false;

    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;

        buildManager = BuildManager.instance;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        rend.material = hoverMaterial;

        if (defense == null)
        {
            if (buildManager.CanBuild)
                buildManager.BuildDefenseOn(this, false);
        }
    }

    void OnMouseExit()
    {
        rend.material = defaultMaterial;
        Destroy(tempDefense);
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (defense != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        Destroy(tempDefense);
        buildManager.BuildDefenseOn(this, true);
        buildManager.SelectDefenseToBuild(null);
        tempDefense = null;
    }

    public void UpgradeDefense()
    {
        if (currentUpgradeState >= defenseClass.upgradeStates.Count - 1)
        {
            Debug.Log("Defense is already maximized!");
            return;
        }

        int upgradeCost = defenseClass.upgradeCosts[currentUpgradeState];
        if (PlayerStats.Money < upgradeCost)
        {
            Debug.Log("Pas assez d'argent pour améliorer!");
            return;
        }

        PlayerStats.Money -= upgradeCost;
        currentUpgradeState++;

        DefenseUpgradeState newState = defenseClass.upgradeStates[currentUpgradeState];

        // Replace the current defense prefab
        if (defense != null)
            Destroy(defense);

        defense = Instantiate(newState.prefab, transform.position + positionOffset, Quaternion.identity);
        defense.transform.parent = transform;

        // Update turret stats
        TurretScript turretScript = defense.GetComponent<TurretScript>();
        if (turretScript != null)
        {
            turretScript.damages = newState.damages;
            turretScript.maximumRange = newState.maximumRange;
            turretScript.fireRate = newState.fireRate;
            turretScript.SetActive(true);
            turretScript.Initialize();
        }

        isUpgraded = true;
        Debug.Log($"Defense améliorée au niveau {currentUpgradeState + 1}!");
    }

}