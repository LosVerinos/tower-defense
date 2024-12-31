using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private Transform target;
    public float speed = 70f;
    public GameObject bulletImpact;
    // Start is called before the first frame update
    public void Find(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            Destroy(gameObject);
            return;
        }


        Vector3 direction = new Vector3(target.position.x, target.position.y + 2.5f, target.position.z) - transform.position;
        float distanceTravelledThisFrame = speed * Time.deltaTime;

        if(direction.magnitude <= distanceTravelledThisFrame){
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * distanceTravelledThisFrame, Space.World);
    }

    void HitTarget(){
        Debug.Log("Hit !");
        GameObject effect = Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
