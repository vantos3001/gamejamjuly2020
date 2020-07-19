using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private ProgressBar _healthBar;

    private void Awake()
    {
        EventManager.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float currentValue)
    {
        var maxHealth = PlayerManager.GetMaxHealth();
        _healthBar.SetValue(currentValue, maxHealth);
    }

    private void OnDestroy()
    {
        EventManager.OnHealthChanged -= OnHealthChanged;
    }
}
