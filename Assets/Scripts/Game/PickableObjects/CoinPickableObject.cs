using System;

public class CoinPickableObject : PickableObject
{
    protected override PickableObjectType Type => PickableObjectType.Coin;
    
    public override void Pick()
    {
        PlayerManager.PlayerData.CurrentCoins++;
        
        base.Pick();
    }
}
