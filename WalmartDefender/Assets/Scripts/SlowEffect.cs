using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


namespace Game
{
    public class SlowEffect : Effect
    {
        public override void ApplyEffect(Node _node)
        {
            base.ApplyEffect(_node);
        }

        void OnTriggerEnter(Collider otherCollider)
        {
            
            if (isActive && otherCollider.CompareTag("Classic Enemy"))
                //Debug.Log("Zombie entre dans les barbelés");
                ApplyEffectSpeed(otherCollider.transform, 0.5f);
        }

        void OnTriggerExit(Collider otherCollider)
        {
            if (isActive && otherCollider.CompareTag("Classic Enemy"))
                Debug.Log("Zombie sort des barbelés");
                ApplyEffectSpeed(otherCollider.transform, 2f);
        }

        void ApplyEffectSpeed(Transform collider, float speedMultiplier)
        {
            NavMeshAgent meshAgent = collider.GetComponent<NavMeshAgent>();

            if (meshAgent != null)
            {
                meshAgent.speed *= speedMultiplier;
            }
        }

    }
}

