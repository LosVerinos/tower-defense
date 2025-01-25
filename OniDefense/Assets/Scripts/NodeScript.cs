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
    [DoNotSerialize]public GameObject defense;
    [DoNotSerialize]public GameObject tempDefense;
    [DoNotSerialize]public DefenseClass defenseClass;
    public Vector3 positionOffset = new Vector3(0f, -0.3f, 0f);
    BuildManager buildManager;

    public bool isUpgraded = false;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;

        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter(){
        if(EventSystem.current.IsPointerOverGameObject())
            return;

        rend.material = hoverMaterial;

        if(defense == null){
            //GameObject defenseToBuild = buildManager.GetDefenseToBuild();
            if(buildManager.CanBuild)
                buildManager.BuildDefenseOn(this, false);
        }
    }

    void OnMouseExit(){
        rend.material = defaultMaterial;
        Destroy(tempDefense);
    }

    void OnMouseDown(){
        if(EventSystem.current.IsPointerOverGameObject())
            return;
        
        if(defense != null){
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        else
        {
            Destroy(tempDefense);
            buildManager.BuildDefenseOn(this, true);
            buildManager.SelectDefenseToBuild(null);
            tempDefense = null;
        }

        
    }

    /*public void UpgradeDefense(NodeScript node)
    {
        if (PlayerStats.Money < defenseClass.upgradeCost);
        {
            Debug.Log("Pas assez d'argent pour améliorer!");
            return;
        }

        PlayerStats.Money -= defenseClass.upgradeCost;

        Destroy(defense); 

        GameObject tempDefense = (GameObject)Instantiate(defenseClass.upgradedPrefab, node.transform.position + node.positionOffset, Quaternion.identity);
        defense = tempDefense;

        isUpgraded = true;

        Debug.Log("Defense améliorée!");
    }*/
}
