using System;

public enum PickableObjectType{
    Coin,
    Corvalol
}

public abstract class PickableObject : IPickableObject
{
    protected abstract PickableObjectType Type { get;}

    public virtual void Pick()
    {
        throw new NotImplementedException();
    }
}
