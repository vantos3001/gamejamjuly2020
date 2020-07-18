using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private TextPanel _coinPanel;

    [SerializeField] private Button _playButton;

    [SerializeField] private List<AbilityPanel> _abilityPanels;
}
