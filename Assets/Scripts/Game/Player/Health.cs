using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float DEFAULT_DAMAGE_PER_SECOND = 1f;
    
    private float _currentHealth = 0;
    private float _currentDamagePerSecond;

    private bool _isFreeze;
    private float _timeBeforeUnfreeze;

    public Action HealthEnded;

    private float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            EventManager.NotifyOnHealthChanged(_currentHealth);
            _currentHealth = value;
        }
    }

    private bool _isLive;

    public void Init(float maxHealth)
    {
        _currentDamagePerSecond = DEFAULT_DAMAGE_PER_SECOND;
        CurrentHealth = maxHealth;
        _isLive = true;
    }

    public void AddHealth(float health)
    {
        CurrentHealth += health;
        
        if(CurrentHealth <= 0)
        {
            Kill();
        }
    }

    public void FreezeDamagePerSecond(float unfreezeTime)
    {
        _currentDamagePerSecond = 0;
        _isFreeze = true;
        _timeBeforeUnfreeze = unfreezeTime;
    }

    private void UnfreezeDamagePerSecond()
    {
        _currentDamagePerSecond = DEFAULT_DAMAGE_PER_SECOND;
        _isFreeze = false;
    }

    private void UpdateFreeze(float delta)
    {
        _timeBeforeUnfreeze -= delta;

        if (_timeBeforeUnfreeze <= 0)
        {
            UnfreezeDamagePerSecond();
        }
    }
    
    private void Update()
    {
        if (_isLive)
        {
            CurrentHealth -= _currentDamagePerSecond * Time.deltaTime;
            
            if (_currentHealth <= 0)
            {
                Kill();
            }
            else
            {
                if (_isFreeze)
                {
                    UpdateFreeze(Time.deltaTime);
                }
            }
        }
    }
    
    public void Kill()
    {
        CurrentHealth = 0;
        _isLive = false;
        HealthEnded?.Invoke();
    }
}
