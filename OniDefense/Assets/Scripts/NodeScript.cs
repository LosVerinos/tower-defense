using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour
{
    private Renderer rend;
    public Material hoverMaterial;
    private Material defaultMaterial;
    private GameObject defense;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseEnter(){
        rend.material = hoverMaterial;
    }

    void OnMouseExit(){
        rend.material = defaultMaterial;
    }

    void OnMouseDown(){
        Debug.Log("Clic !");

        if(defense != null){
            Debug.Log("Can't build there"); //A aouter en message à l'ecran
            return;
        }
    }
}
