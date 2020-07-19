using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private TextPanel _coinPanel;

    [SerializeField] private Button _playButton;

    [SerializeField] private List<AbilityPanel> _abilityPanels;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButton);
    }

    public void SetCoinPanel(string text)
    {
        _coinPanel.SetText("Coins: " + text);
    }

    public void SetAbilityPanel(Ability ability)
    {
        var abilityPanel = _abilityPanels.Find(ap => ap.AbilityType == ability.AbilityType);

        if (abilityPanel != null)
        {
            abilityPanel.SetContent(ability);
        }
        else
        {
            Debug.LogError("AbilityPanel with type = " + ability.AbilityType + " is not found");
        }
    }

    private void OnPlayButton()
    {
        SceneManager.LoadScene("Gameplay");
    }

    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(OnPlayButton);
    }
}
