using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    private Renderer rend;
    public Material hoverMaterial;
    private Material defaultMaterial;
    private GameObject defense;
    private GameObject tempDefense;
    private Vector3 positionOffset = new Vector3(0f, -0.3f, 0f);
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
        rend.material = hoverMaterial;
        if(defense == null){
            GameObject defenseToBuild = buildManager.GetDefenseToBuild();
            if(defenseToBuild != null)
                tempDefense = Instantiate(defenseToBuild, transform.position + positionOffset, transform.rotation);
        }
    }

    void OnMouseExit(){
        rend.material = defaultMaterial;
        Destroy(tempDefense);
    }

    void OnMouseDown(){
        
        if(buildManager.GetDefenseToBuild() == null)
            return;

        if(defense != null){
            //TODO: Ajouter UI avec stat de la case
            //DisplayNodeInfo()
            Debug.Log("Can't build there"); //A aouter en message à l'ecran
            return;
        }
        else{
            Destroy(tempDefense);
            //TODO: Choix de la défense
            GameObject defenseToBuild = buildManager.GetDefenseToBuild();
            defense = Instantiate(defenseToBuild, transform.position + positionOffset, transform.rotation);
            TurretScript defenseScript = defense.GetComponent<TurretScript>();
            defenseScript.SetActive(true);
            buildManager.SetDefenseToBuild(null);
            tempDefense = null;
        }
    }
}
