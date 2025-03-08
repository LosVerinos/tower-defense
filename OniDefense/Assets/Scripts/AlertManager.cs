using System.Collections;
using UnityEngine;

public class ZombieAlertManager : MonoBehaviour
{
    public static ZombieAlertManager Instance;
    public GameObject alertPrefab;
    public Transform alertParent; // Canvas Parent

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowAlert(Vector3 position)
    {
        GameObject alert = Instantiate(alertPrefab, alertParent);
        alert.transform.position = Camera.main.WorldToScreenPoint(position);

        Animator animator = alert.GetComponent<Animator>();
        if (animator != null)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Alert_Show")) // ✅ Empêche de rejouer l'animation
            {
                animator.SetTrigger("Show");
                StartCoroutine(HideAfterDelay(animator, alert));
            }
        }

        
    }

    private IEnumerator HideAfterDelay(Animator animator, GameObject alert)
    {
        Debug.Log("⌛ L'alerte va disparaître dans 2 secondes...");
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("Hide");
        Debug.Log("🛑 Transition vers `Alert_Hide`");
        yield return new WaitForSeconds(1f);
        Destroy(alert);
    }
}
