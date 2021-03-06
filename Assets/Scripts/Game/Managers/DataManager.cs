﻿using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    private static Dictionary<AbilityType, AbilityConfig> _cachedAbilityConfigs = new Dictionary<AbilityType, AbilityConfig>();

    private static GameplayConfig _gameplayConfig;
    
    public static AbilityConfig GetAbilityConfig(AbilityType abilityType)
    {
        AbilityConfig abilityConfig = null;
        if (_cachedAbilityConfigs.TryGetValue(abilityType, out abilityConfig))
        {
            return abilityConfig;
        }

        abilityConfig = Resources.Load<AbilityConfig>($"Configs/{abilityType}AbilityConfig");

        if (abilityConfig == null)
        {
            Debug.LogError("AbilityConfig with type = " + abilityType + "is not found");
        }
        else
        {
            _cachedAbilityConfigs.Add(abilityType, abilityConfig);
        }

        return abilityConfig;
    }

    public static GameplayConfig GetGameplayConfig()
    {
        if (_gameplayConfig != null)
        {
            return _gameplayConfig;
        }
        
        _gameplayConfig = Resources.Load<GameplayConfig>($"Configs/GameplayConfig");

        if (_gameplayConfig == null)
        {
            Debug.LogError("GameplayConfig is not found");
        }

        return _gameplayConfig;
    }
}
