using System.Collections;
using Game;
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

    public void ShowAlert(Transform zombieSpawnLocation)
    {
        if (alertPrefab == null || alertParent == null)
        {
            return;
        }
        GameObject alert = Instantiate(alertPrefab, alertParent);

        alert.GetComponent<RectTransform>().anchoredPosition = new Vector2(zombieSpawnLocation.position.x*10, 490);
        Animator animator = alert.GetComponent<Animator>();

        Destroy(alert, 2f);
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
