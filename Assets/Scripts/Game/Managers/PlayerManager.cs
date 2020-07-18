using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerManager
{
    private static PlayerData _playerData;
    public static PlayerData PlayerData => _playerData;
    
    public static void Init()
    {
        CreatePlayer();
    }

    private static void CreatePlayer()
    {
        _playerData = new PlayerData();
        
        var clickTimeAbility = new Ability(AbilityType.ClickTime);
        var maxHealthAbility = new Ability(AbilityType.MaxHealth);
        
        _playerData.Abilities.Add(clickTimeAbility);
        _playerData.Abilities.Add(maxHealthAbility);
    }
}
