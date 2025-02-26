using System.Collections;
using UnityEngine;

public class AirStrikeEffect : Effect
{
    public GameObject obusPrefab;
    public float spawnHeight = 20f;
    private float delayBeforeDrop = 5f;
    public GameObject targetObject;
    public float spawnRadius = 15f;
    
    public override void ApplyEffect(NodeScript _node)
    {
        base.ApplyEffect(_node);
        targetObject.SetActive(false);
        InvokeRepeating("DropObus", delayBeforeDrop, 0.1f);
        StartCoroutine(ReleaseNode());
    }

    void DropObus()
    {
        if (obusPrefab != null)
        {
            // Générer une position aléatoire dans un rayon autour du centre de la node
            Vector3 randomOffset = Random.insideUnitSphere * spawnRadius;
            randomOffset.y = 0f; // Ne pas modifier la hauteur (y) de la node
            Vector3 spawnPosition = new Vector3(node.transform.position.x, spawnHeight, node.transform.position.z) + randomOffset;
            GameObject obus = Instantiate(obusPrefab, spawnPosition, Quaternion.identity);
            ObusScript obusScript = obus.GetComponent<ObusScript>();

            if (obusScript != null)
            {
                obusScript.SetSpeed(40f);
                obusScript.SetDamage(50f);
                obusScript.SetAerialLaunch(true);
                obusScript.SetTarget(node.transform); // On vise la node actuelle
            }
        }

         // Supprime l'effet après l'impact
    }

    IEnumerator ReleaseNode()
    {
        yield return new WaitForSeconds(8f);  // Attendre un instant après l’explosion
        RemoveEffect();
        node.defense = null;  // Libérer la node
        Destroy(gameObject);  // Détruire l'objet AirStrikeEffect
    }
}
