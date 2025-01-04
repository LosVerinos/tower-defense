using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField]private float health;
    [SerializeField]private int reward;
    // Start is called before the first frame update
    void Start()
    {
        
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
        PlayerStats.Money += reward;
        Debug.Log("Zombie tué ! +10$");
    }
}
