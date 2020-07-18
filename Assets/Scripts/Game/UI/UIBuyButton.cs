using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuyButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _costText;

    public Action ButtonClicked;

    private void Awake()
    {
        _button.onClick.AddListener(NotifyButtonClicked);
    }

    public void SetText(string text)
    {
        _costText.text = text;
    }
    
    private void NotifyButtonClicked()
    {
        ButtonClicked?.Invoke();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(NotifyButtonClicked);
    }
}
