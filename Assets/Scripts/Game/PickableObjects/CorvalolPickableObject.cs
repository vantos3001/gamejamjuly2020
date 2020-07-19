using System;

public class CorvalolPickableObject : PickableObject
{
    protected override PickableObjectType Type => PickableObjectType.Corvalol;

    public override void Pick()
    {
        BonusManager.UseCorvalol();
        
        base.Pick();
    }
}
