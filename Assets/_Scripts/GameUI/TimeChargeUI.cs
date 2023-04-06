using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeChargeUI : MonoBehaviour
{
    [SerializeField] private Image _imageFillTimeValue;
    [SerializeField] private TimeChanger _timeChanger;

    private void Update()
    {
        _imageFillTimeValue.fillAmount = _timeChanger.NormalizeValueTimeCharge;
    }
}
