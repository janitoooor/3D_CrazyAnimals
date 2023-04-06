using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DamageScreenUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [Space]
    [SerializeField] private float _timeToHide = 1f;

    private PlayerHealth _playerHealth;

    [Inject]
    public void Constructor(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Start()
    {
        _canvasGroup.alpha = 0;

        _playerHealth.OnPlayerTakeDamage += PlayerHealth_OnPlayerTakeDamage;
    }

    private void PlayerHealth_OnPlayerTakeDamage(object sender, System.EventArgs e)
    {
        StartEffectTakeDamage();
    }

    public void StartEffectTakeDamage()
    {
        StartCoroutine(ShowEffect());
    }

    private IEnumerator ShowEffect()
    {
        _canvasGroup.alpha = 1;
        for (float t = _timeToHide; t > 0; t -= Time.deltaTime)
        {
            _canvasGroup.alpha = t;
            yield return null;
        }
    }
}
