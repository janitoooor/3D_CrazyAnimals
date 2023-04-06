using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimeChanger : MonoBehaviour
{
    [SerializeField] private float _timeScale = 0.2f;
    [SerializeField] private float _timeMultiplyCharge = 3f;
    [SerializeField] private float _timeMaxDuration = 4f;
    [SerializeField] private float _minTimeToCanUsetTimeChanger = 0.5f;
    [Space]
    [Space]
    [SerializeField] private CanvasGroup _timeChangePanelGroup;

    public float NormalizeValueTimeCharge => 1 - Mathf.Abs(_timer) / _timeMaxDuration;

    private float _startFixedDeltaTime;

    private bool _buttonIsPressed;

    private float _timer;

    private GameInput _gameInput;

    [Inject]
    public void Constructor(GameInput gameInput)
    {
        _gameInput = gameInput;
    }

    #region Mono
    private void Start()
    {
        _startFixedDeltaTime = Time.fixedDeltaTime;

        _gameInput.OnMouseClickTimeSlowAction += GameInput_OnMouseClickTimeSlowAction;
        _gameInput.OnMouseUnClickTimeSlowAction += GameInput_OnMouseUnClickTimeSlowAction;

        _timeChangePanelGroup.alpha = 0;
    }

    private void Update()
    {
        if (_buttonIsPressed)
            SlowTime();
        else
            NormalizeTime();

        Time.fixedDeltaTime = _startFixedDeltaTime * Time.timeScale;
    }

    private void OnDestroy()
    {
        Time.fixedDeltaTime = _startFixedDeltaTime;
    }

    #endregion

    private void SlowTime()
    {
        if (HasCharge())
            _timer += Time.unscaledDeltaTime;

        if (NotMinCharge())
        {
            Time.timeScale = _timeScale;
            _timeChangePanelGroup.alpha = NormalizeValueTimeCharge;
        }
        else if (IsEmptyCharge())
        {
            Time.timeScale = 1;
        }

    }

    private bool NotMinCharge()
    {
        return _timer + _minTimeToCanUsetTimeChanger < _timeMaxDuration;
    }

    private bool IsEmptyCharge()
    {
        return _timer < _timeMaxDuration;
    }

    private bool HasCharge()
    {
        return _timer < _timeMaxDuration + _minTimeToCanUsetTimeChanger;
    }

    private void NormalizeTime()
    {
        if (NeedCharge())
            _timer -= Time.unscaledDeltaTime / _timeMultiplyCharge;

        if (HasAlpha())
            _timeChangePanelGroup.alpha -= Time.unscaledDeltaTime;

        Time.timeScale = 1;
    }

    private bool NeedCharge()
    {
        return _timer > 0;
    }

    private bool HasAlpha()
    {
        return _timeChangePanelGroup.alpha > 0;
    }

    private void GameInput_OnMouseUnClickTimeSlowAction(object sender, System.EventArgs e)
    {
        _buttonIsPressed = false;
    }

    private void GameInput_OnMouseClickTimeSlowAction(object sender, System.EventArgs e)
    {
        _buttonIsPressed = true;
    }
}
