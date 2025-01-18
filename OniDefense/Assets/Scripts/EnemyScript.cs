using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{

    [SerializeField]private float baseHealth;
    [SerializeField]private int reward;

    public bool speedReduced;
    private float agentSpeed;
    private float health;
    public UnityEngine.UI.Image healthBar;
    private Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        agentSpeed = transform.GetComponent<NavMeshAgent>().speed;
        canvas = GetComponentInChildren<Canvas>();
        health = baseHealth;
        InvokeRepeating("ResetSpeed", 3f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void TakeDamages(float damages){
        health -= damages;
        canvas.enabled = true;
        healthBar.fillAmount = health/baseHealth;
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
