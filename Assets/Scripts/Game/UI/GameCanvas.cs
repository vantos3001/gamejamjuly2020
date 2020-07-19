using System;
using UnityEditor;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private TextPanel _distancePanel;
    [SerializeField] private TextPanel _restPanel;

    private void Awake()
    {
        EventManager.OnHealthChanged += OnHealthChanged;
        EventManager.OnDistanceChanged += OnDistanceChanged;
        EventManager.OnRestClickTimeChanged += OnRestClickTimeChanged;
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

    private void OnRestClickTimeChanged(float restClickTime)
    {
        if (restClickTime <= 0 || PlayerManager.Health.IsFreeze)
        {
            _restPanel.gameObject.SetActive(false);
        }
        else
        {
            _restPanel.gameObject.SetActive(true);
            _restPanel.SetText("Rest: " + restClickTime.ToString("0.0") +  "s");
        }
    }

    private void OnDestroy()
    {
        EventManager.OnHealthChanged -= OnHealthChanged;
        EventManager.OnDistanceChanged -= OnDistanceChanged;
        EventManager.OnRestClickTimeChanged -= OnRestClickTimeChanged;
    }
}
