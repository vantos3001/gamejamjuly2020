using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BonusManager
{
    public static void UseCorvalol()
    {
        PlayerManager.Health.FreezeDamagePerSecond(DataManager.GetGameplayConfig().CarvalolFreezeTime);
    }
}
