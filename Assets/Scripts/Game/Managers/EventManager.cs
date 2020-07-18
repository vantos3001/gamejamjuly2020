using System;

public static class EventManager
{
    public static Action<AbilityType> OnAbilityLevelUp;
    
    public static void NotifyOnAbilityLevelUp(AbilityType abilityType)
    {
        OnAbilityLevelUp?.Invoke(abilityType);
    }
}
