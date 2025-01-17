using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EffectZoneScript : MonoBehaviour
{
    private bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        Collider[] excolliders = Physics.OverlapBox(transform.position, new Vector3(4.01f, 4.01f, 4.01f));
                foreach(Collider excollider in excolliders){
                    if(excollider.tag == "zombie"){
                        Debug.Log("Zombie impacté par l'effet");
                        ApplyEffectSpeed(excollider.transform, 6f);
                    }
                }

        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(4f, 4f, 4f));
                foreach(Collider collider in colliders){
                    if(collider.tag == "zombie"){
                        Debug.Log("Zombie impacté par l'effet");
                        ApplyEffectSpeed(collider.transform, 0.5f);
                    }
                }
        */
        
    }

    void OnTriggerEnter(Collider otherCollider){
        Debug.Log("Trigger Enter");
        if(!active)
            return;
        if(otherCollider.transform.tag == "zombie")
            Debug.Log("Zombie entre dans les barbelés");
            ApplyEffectSpeed(otherCollider.transform, 0.5f);
    }

    void OnTriggerExit(Collider otherCollider){
        Debug.Log("Trigger Exit");
        if(!active)
            return;
        if(otherCollider.transform.tag == "zombie")
            Debug.Log("Zombie sort des barbelés");
            ApplyEffectSpeed(otherCollider.transform, 2f);
    }

    void ApplyEffectSpeed(Transform collider, float speedMultiplier){
        NavMeshAgent meshAgent = collider.GetComponent<NavMeshAgent>();
        if(meshAgent != null){
            meshAgent.speed *= speedMultiplier;
        }
        /*EnemyScript e = collider.GetComponent<EnemyScript>();
        Debug.Log(e.speedReduced);
        if(speedMultiplier < 1f && !e.speedReduced){
            meshAgent.speed *= speedMultiplier;
            e.speedReduced = true;
            return;
        }
        if(speedMultiplier > 1f && e.speedReduced){
            meshAgent.speed *= speedMultiplier;
            e.speedReduced = false;
            return;
        }*/
        
    }

    public void SetActive(bool _activation){
        active = _activation;
    }
}
