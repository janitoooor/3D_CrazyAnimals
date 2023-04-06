using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlinkEffect : BlinkEffect
{
    [SerializeField] private EnemyHealth _enemyHealth;

    #region Mono
    private void Start()
    {
        _enemyHealth.OnEnemyTakeDamage.AddListener(() =>
        {
            StartBlink();
        });
    }
    #endregion
}
