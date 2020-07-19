using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float DEFAULT_DAMAGE_PER_SECOND = 1f;
    
    private float _currentHealth = 0;
    private float _currentDamagePerSecond;

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

    public void FreezeDamagePerSecond()
    {
        _currentDamagePerSecond = 0;
    }

    public void UnfreezeDamagePerSecond()
    {
        _currentDamagePerSecond = DEFAULT_DAMAGE_PER_SECOND;
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
        }
    }
    
    public void Kill()
    {
        CurrentHealth = 0;
        _isLive = false;
        HealthEnded?.Invoke();
    }
}
