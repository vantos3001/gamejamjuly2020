using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    [SerializeField] private string _textPrefix;
    [SerializeField] private TextMeshProUGUI _abilityText;
    [SerializeField] private UIBuyButton _buyButton;

    [SerializeField] private AbilityType _abilityType;
    public AbilityType AbilityType => _abilityType;

    public void SetText(string text)
    {
        _abilityText.text = _textPrefix + text;
    }
}
