using System;
using UnityEngine;

public enum PickableObjectType{
    Coin,
    Corvalol
}

public abstract class PickableObject : MonoBehaviour, IPickableObject
{
    protected abstract PickableObjectType Type { get;}

    public virtual void Pick()
    {
        Destroy(gameObject);
    }
}
