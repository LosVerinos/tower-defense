using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{

    [SerializeField] public float baseHealth;
    [SerializeField] public int reward;
    [SerializeField] public int damage;
    [SerializeField] public float difficultyWeight;
    public float health;
    public UnityEngine.UI.Image healthBar;
    private Canvas canvas;
    public NavMeshAgent agent;

    protected virtual void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        health = baseHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void TakeDamages(float damages)
    {
        health -= damages;
        canvas.enabled = true;
        healthBar.fillAmount = health / baseHealth;
        if (health <= 0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        PlayerStats.Money += reward * PlayerStats.moneyMultiplier;
        PlayerStats.NbKilledEmenies++;
        //Debug.Log("Zombie tué ! +" + reward * PlayerStats.moneyMultiplier + "$ ! Monnaie actuelle : " + PlayerStats.Money);
        WaveSpawner.EnemyDied();
    }
}
