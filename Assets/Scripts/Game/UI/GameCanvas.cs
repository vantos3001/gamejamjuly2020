using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private TextPanel _distancePanel;

    private void Awake()
    {
        EventManager.OnHealthChanged += OnHealthChanged;
        EventManager.OnDistanceChanged += OnDistanceChanged;
    }

    private void OnHealthChanged(float currentValue)
    {
        var maxHealth = PlayerManager.GetMaxHealth();
        _healthBar.SetValue(currentValue, maxHealth);
    }

    private void OnDistanceChanged(float distance)
    {
        _distancePanel.SetText("Distance: " + distance);
    }

    private void OnDestroy()
    {
        EventManager.OnHealthChanged -= OnHealthChanged;
        EventManager.OnDistanceChanged -= OnDistanceChanged;
    }
}
