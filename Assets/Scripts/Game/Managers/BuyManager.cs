using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BuyManager
{
    public static void TryUpgradeAbility(AbilityType abilityType)
    {
        Ability ability = PlayerManager.PlayerData.Abilities.Find(ab => ab.AbilityType == abilityType);
        
        if (!ability.IsMaxLevel && IsCoinsEnough(ability.NextData.Cost))
        {
            PlayerManager.PlayerData.CurrentCoins -= ability.NextData.Cost;
            ability.LevelUp();
            EventManager.NotifyOnAbilityLevelUp(abilityType);
        }
        
    }
    
    private static bool IsCoinsEnough(int needCoins)
    {
        return needCoins <= PlayerManager.PlayerData.CurrentCoins;
    }
}
