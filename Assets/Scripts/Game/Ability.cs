﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    ClickTime,
    MaxHealth
}
public class Ability
{
    private AbilityType _abilityType;
    private AbilityData _data;

    private AbilityConfig _config;

    private int _currentLevel = 0;
    
    public Ability(AbilityType abilityType)
    {
        _abilityType = abilityType;

        _config = DataManager.GetAbilityConfig(_abilityType);
        
        SetData();
    }

    private void SetData()
    {

        if (_currentLevel < _config.AbilityDatas.Count)
        {
            _data = _config.AbilityDatas[_currentLevel];
        }
        else
        {
            Debug.LogError("Not found abilityData with type = " + _abilityType + "; level = " + _currentLevel);
        }
    }

}

[Serializable]
public class AbilityData
{
    public float Value;
    public int Cost;
}