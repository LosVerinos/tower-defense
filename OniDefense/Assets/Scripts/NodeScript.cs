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
    public Vector3 positionOffset;
    BuildManager buildManager;


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
        
        if(!buildManager.CanBuild)
            return;

        if(defense != null){
            //TODO: Ajouter UI avec stat de la case
            //DisplayNodeInfo()
            Debug.Log("Can't build there"); //A aouter en message à l'ecran
            return;
        }
        else{
            Destroy(tempDefense);
            buildManager.BuildDefenseOn(this, true);
            buildManager.SelectDefenseToBuild(null);
            tempDefense = null;
        }
    }
}
