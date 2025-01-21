using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{

    [SerializeField] protected float baseHealth;
    [SerializeField] protected int reward;    
    protected float health;
    protected bool speedReduced;
    protected float agentSpeed;
    public UnityEngine.UI.Image healthBar;
    private Canvas canvas;
    protected NavMeshAgent agent;

    protected virtual void Start()
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

    public virtual void TakeDamages(float damages){
        health -= damages;
        canvas.enabled = true;
        healthBar.fillAmount = health/baseHealth;
        if(health <= 0)
            Die();
    }

    protected virtual void Die(){
        Destroy(gameObject);
        PlayerStats.Money += reward * PlayerStats.moneyMultiplier;
        Debug.Log("Zombie tué ! +" + reward * PlayerStats.moneyMultiplier + "$ ! Monnaie actuelle : " + PlayerStats.Money);
    }

    protected void ResetSpeed(){
        if(speedReduced){
            speedReduced = false;
            transform.GetComponent<NavMeshAgent>().speed = agentSpeed;
        }
    }
}
