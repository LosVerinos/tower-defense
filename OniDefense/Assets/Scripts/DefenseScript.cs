using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseScript : MonoBehaviour
{
    private float currentHealth;
    private DefenseClass defenseData;
    private float baseHealth;
    public UnityEngine.UI.Image healthBar;
    private Canvas canvas;

    public void Initialize(DefenseClass data)
    {
        defenseData = data;
        baseHealth = defenseData.health;
        currentHealth = baseHealth;
        canvas = GetComponentInChildren<Canvas>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        canvas.enabled = true;
        healthBar.fillAmount = currentHealth/baseHealth;
        
        Debug.Log(gameObject.name + " subit " + amount + " dégâts. Vie restante: " + currentHealth);
        StartCoroutine(EraseHealthBar());
        if (currentHealth <= 0)
        {
            DestroyDefense();
        }
    }

    void DestroyDefense()
    {
        Debug.Log(gameObject.name + " est détruite !");
        Destroy(gameObject);
    }


    IEnumerator EraseHealthBar()
    {
        yield return new WaitForSeconds(5f);  // Attendre un instant après l’explosion
        canvas.enabled = false;
    }
}
