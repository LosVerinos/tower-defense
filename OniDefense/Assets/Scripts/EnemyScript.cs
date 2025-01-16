using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{

    [SerializeField]private float health;
    [SerializeField]private int reward;

    public bool speedReduced;
    private float agentSpeed;
    // Start is called before the first frame update
    void Start()
    {
        agentSpeed = transform.GetComponent<NavMeshAgent>().speed;
        InvokeRepeating("ResetSpeed", 3f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void TakeDamages(float damages){
        health -= damages;
        if(health <= 0)
            Die();
    }

    void Die(){
        Destroy(gameObject);
        PlayerStats.Money += reward * PlayerStats.moneyMultiplier;
        Debug.Log("Zombie tué ! +" + reward * PlayerStats.moneyMultiplier + "$ ! Monnaie actuelle : " + PlayerStats.Money);
    }

    void ResetSpeed(){
        if(speedReduced){
            speedReduced = false;
            transform.GetComponent<NavMeshAgent>().speed = agentSpeed;
        }
    }
}
