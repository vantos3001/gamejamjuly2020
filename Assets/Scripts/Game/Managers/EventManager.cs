﻿using System;

public static class EventManager
{
    public static Action<AbilityType> OnAbilityLevelUp;

    public static Action<float> OnHealthChanged;
    
    public static void NotifyOnAbilityLevelUp(AbilityType abilityType)
    {
        OnAbilityLevelUp?.Invoke(abilityType);
    }

    public static void NotifyOnHealthChanged(float currentHealth)
    {
        OnHealthChanged?.Invoke(currentHealth);
    }
}
