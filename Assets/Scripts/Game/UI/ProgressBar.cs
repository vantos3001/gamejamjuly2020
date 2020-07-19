using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fill;

    public void SetValue(float value, float maxValue)
    {
        var fillAmount = value / maxValue;

        _fill.fillAmount = fillAmount;
    }
}
