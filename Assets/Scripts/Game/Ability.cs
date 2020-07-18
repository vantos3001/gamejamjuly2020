using System;
using UnityEngine;

public enum AbilityType
{
    ClickTime,
    MaxHealth
}
public class Ability
{
    private AbilityType _abilityType;
    public AbilityType AbilityType => _abilityType;

    private AbilityData _data;
    public AbilityData Data => _data;

    private AbilityConfig _config;

    private int _currentLevel = 0;
    private int MaxLevel => _config.AbilityDatas.Count - 1;
    public bool IsMaxLevel => MaxLevel <= _currentLevel;
    public AbilityData NextData => _config.AbilityDatas[_currentLevel + 1];
    
    public Ability(AbilityType abilityType)
    {
        _abilityType = abilityType;

        _config = DataManager.GetAbilityConfig(_abilityType);
        
        UpdateData();
    }

    private void UpdateData()
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

    public void LevelUp()
    {
        if (IsMaxLevel) return;
        
        _currentLevel++;
        UpdateData();
    }

}

[Serializable]
public class AbilityData
{
    public float Value;
    public int Cost;
}
