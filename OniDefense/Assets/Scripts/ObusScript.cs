using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObusScript : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public GameObject bulletImpact;
    public float damagesRadius;
    private float damages;
    private Vector3 destination;
    private Vector3 startPosition;
    private float flightTime; // Temps total de vol
    private float elapsedTime; // Temps écoulé depuis le départ
    public float arcHeight; // Hauteur maximale de l'arc
    private Vector3 previousPosition;
    private bool aerialLaunch = false;

    public void Find(Transform _target)
    {
        target = _target;

        destination = new Vector3(target.position.x, 0, target.position.z);
        startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, destination);
        flightTime = distance / speed;

        elapsedTime = 0f;

        previousPosition = startPosition;
    }

    public void SetDamage(float _damages){
        damages = _damages;
    }

    void Update()
    {
        if(!aerialLaunch){
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= flightTime || transform.position.y < 0f)
            {
                Explode();
                return;
            }

            float progress = elapsedTime / flightTime;
            Vector3 horizontalPosition = Vector3.Lerp(startPosition, destination, progress);

            float verticalOffset = Mathf.Sin(progress * Mathf.PI) * arcHeight;

            Vector3 newPosition = new Vector3(horizontalPosition.x, horizontalPosition.y + verticalOffset, horizontalPosition.z);
            transform.position = newPosition;

            Vector3 direction = (newPosition - previousPosition).normalized;

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
            previousPosition = newPosition;
        }
        else{
            transform.position += Vector3.down * speed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
            if (transform.position.y <= 2f)
            {
                Explode();
            }
        }
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, damagesRadius);
        foreach(Collider collider in colliders){
            if(collider.tag == "zombie" || collider.tag == "destructible"){
                Debug.Log("Zombie touché par l'explosion !");
                Damage(collider.transform);
            }
        }
        GameObject effect = Instantiate(bulletImpact, transform.position, Quaternion.Euler(-90, 0, 0));
        Destroy(effect, 2f);
        Destroy(gameObject);
    }

    void Damage(Transform colliderTransform){
        EnemyBase e = colliderTransform.GetComponent<EnemyBase>();
            if(e != null){
                e.TakeDamages(damages);
            }
        DefenseScript defense = colliderTransform.GetComponent<DefenseScript>();
            if (defense != null)
            {
                defense.TakeDamage(damages);
            }
    }

    public void SetAerialLaunch(bool _aerialLaunch){
        aerialLaunch = _aerialLaunch;
    }

    public void SetSpeed(float _newSpeed){
        speed = _newSpeed;
    }
}
