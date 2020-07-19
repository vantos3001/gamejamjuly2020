using System;

public static class EventManager
{
    public static Action<AbilityType> OnAbilityLevelUp;

    public static Action<float> OnHealthChanged;
    public static Action<float> OnDistanceChanged;
    public static Action<float> OnRestClickTimeChanged;
    
    public static void NotifyOnAbilityLevelUp(AbilityType abilityType)
    {
        OnAbilityLevelUp?.Invoke(abilityType);
    }

    public static void NotifyOnHealthChanged(float currentHealth)
    {
        OnHealthChanged?.Invoke(currentHealth);
    }
    
    public static void NotifyOnDistanceChanged(float currentDistance)
    {
        OnDistanceChanged?.Invoke(currentDistance);
    }
    public static void NotifyOnOnRestClickTimeChanged(float currentRestClickTime)
    {
        OnRestClickTimeChanged?.Invoke(currentRestClickTime);
    }
}
