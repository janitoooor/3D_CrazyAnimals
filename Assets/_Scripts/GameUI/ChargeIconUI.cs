using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargeIconUI : MonoBehaviour
{
    [SerializeField] private Image _imageBackground;
    [SerializeField] private Image _imageForeground;
    [SerializeField] private TextMeshProUGUI _currentChargeText;

    private Color _imageBackgroundColorOnCharge = new (1f, 1f, 1f, 0.2f);
    private Color _imageBackgroundColorNormal;

    private void Start()
    {
        _imageBackgroundColorNormal = _imageBackground.color;
    }

    public void StartCharge()
    {
        _imageBackground.color = _imageBackgroundColorOnCharge;
        _imageForeground.enabled = true;
        _currentChargeText.enabled = true;
    }

    public void StopCharge()
    {
        _imageBackground.color = _imageBackgroundColorNormal;
        _imageForeground.enabled = false;
        _currentChargeText.enabled = false;
    }

    public void SetChargeValue(float currentCharge, float maxCharge)
    {
        _imageForeground.fillAmount = currentCharge / maxCharge;
        _currentChargeText.text = Mathf.Ceil(maxCharge - currentCharge).ToString();
    }
}
