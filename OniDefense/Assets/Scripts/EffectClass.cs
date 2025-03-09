using UnityEngine;

namespace Game
{
    public abstract class Effect : MonoBehaviour
    {
        protected bool isActive = false;
        protected Node node;

        public virtual void ApplyEffect(Node _node)
        {
            node = _node;
            isActive = true;
        }

        public virtual void RemoveEffect()
        {
            isActive = false;
        }
    }
}

