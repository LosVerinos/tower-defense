using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DefenseScript : MonoBehaviour
{
    private float currentHealth;
    private DefenseClass defenseData;
    private float baseHealth;
    public Image healthBar;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Coroutine fadeCoroutine;
    private float lastDamageTime;

    public void Initialize(DefenseClass data)
    {
        defenseData = data;
        baseHealth = defenseData.upgradeStates[defenseData.upgradeLevel].health;
        currentHealth = baseHealth;
        canvas = GetComponentInChildren<Canvas>();
        //Debug.Log(defenseData.upgradeStates[defenseData.upgradeLevel].prefab.name + " Canvas = " + canvas);
        canvasGroup = canvas.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = canvas.gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        lastDamageTime = Time.time;

        canvas.enabled = true;
        canvasGroup.alpha = 1f;
        healthBar.fillAmount = currentHealth / baseHealth;

        //Debug.Log(gameObject.name + " subit " + amount + " dégâts. Vie restante: " + currentHealth);

        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }

        StartCoroutine(StartFadeDelay());

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

    IEnumerator StartFadeDelay()
    {
        yield return new WaitForSeconds(5f); // Attendre 5 secondes après le dernier dégât

        if (Time.time - lastDamageTime >= 5f)
        {
            fadeCoroutine = StartCoroutine(FadeOutHealthBar());
        }
    }

    IEnumerator FadeOutHealthBar()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Si des dégâts sont reçus pendant le fade, interrompre
            if (Time.time - lastDamageTime < 5f)
            {
                canvasGroup.alpha = 1f;
                fadeCoroutine = null;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            yield return null;
        }

        canvas.enabled = false; // Désactiver le Canvas une fois totalement invisible
        fadeCoroutine = null;
    }
    /*
    void OnMouseEnter()
    {
        //Debug.Log("OnMouseEnter défense !");
        NodeScript node = transform.GetComponentInParent<NodeScript>();
        //Debug.Log(node);
        if (node != null)
        {
            //Debug.Log("Setting hover TRUE !");
            node.SetHover(true);
        }
    }

    void OnMouseExit()
    {
        NodeScript node = transform.GetComponentInParent<NodeScript>();
        if (node != null)
        {
            node.SetHover(false);
        }
    }

    void OnMouseDown(){
        NodeScript node = transform.GetComponentInParent<NodeScript>();
        if (node != null)
        {
            node.OnMouseDown();
        }
    }*/
    
}
