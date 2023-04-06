using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class BlinkEffect : MonoBehaviour
{
    private protected const string EMISSON_COLOR = "_EmissionColor";

    [SerializeField] private protected Renderer[] _targetRenderers;
    [Space]
    [SerializeField] private protected float _timeDurationEffect = 1f;

    private protected readonly float _speedBlinkEffect = 30f;
    private protected readonly float _valueToUpSinFunction = 0.5f;

    private protected readonly Color _colorEmissonBase = new(0, 0, 0, 0);

    #region Mono
    private protected virtual void OnEnable()
    {
        SetEmissonOnAllRenderers(_colorEmissonBase);
    }

    #endregion

    public virtual void StartBlink()
    {
        if (_targetRenderers != null)
            StartCoroutine(BlinkEffectTakeDamage());
    }

    private IEnumerator BlinkEffectTakeDamage()
    {
        for (float t = 0; t < _timeDurationEffect; t += Time.deltaTime)
        {
            Color colorEmisson = new(Mathf.Sin(t * _speedBlinkEffect) * _valueToUpSinFunction + _valueToUpSinFunction, 0, 0, 0);
            SetEmissonOnAllRenderers(colorEmisson);

            yield return null;
        }
    }

    private protected virtual void SetEmissonOnAllRenderers(Color color)
    {
        foreach (var renderer in _targetRenderers)
        {
            foreach (var material in renderer.materials)
            {
                material.SetColor(
                     EMISSON_COLOR, color
                );
            }
        }
    }
}
