using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    protected bool isActive = false;
    protected NodeScript node;

    public virtual void ApplyEffect(NodeScript _node)
    {
        node = _node;
        isActive = true;
    }

    public virtual void RemoveEffect()
    {
        isActive = false;
    }
}