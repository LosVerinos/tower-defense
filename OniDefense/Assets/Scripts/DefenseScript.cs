using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseScript : MonoBehaviour
{
    public float currentHealth;
    private DefenseClass defenseData;
    private float baseHealth;
    public UnityEngine.UI.Image healthBar;
    private Canvas canvas;

    public void Initialize(DefenseClass data)
    {
        defenseData = data;
        currentHealth = defenseData.health;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        canvas.enabled = true;
        healthBar.fillAmount = currentHealth/baseHealth;
        Debug.Log(gameObject.name + " subit " + amount + " dégâts. Vie restante: " + currentHealth);

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

}
