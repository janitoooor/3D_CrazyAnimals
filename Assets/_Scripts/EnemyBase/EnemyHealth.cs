using QFSW.MOP2;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class EnemyHealth : MonoBehaviour, IHealth
{
    [HideInInspector] public UnityEvent OnEnemyTakeDamage;
    [SerializeField] private PrefabCreator _prefabCreator;
    [Space]
    [SerializeField] private int _health = 1;
    [SerializeField] private bool _isProjectile;

    private int _startHealth;

    private void Awake()
    {
        _startHealth = _health;
    }

    public void TakeDamage(int damageValue)
    {
        _health -= damageValue;

        SoundsEffect.Instance.PlaySoundHitEnemy(this);
        OnEnemyTakeDamage.Invoke();

        if (_health < 0)
            Die();
    }

    private void Die()
    {
        if (_isProjectile && gameObject.activeInHierarchy)
        {
            _health = _startHealth;
            MasterObjectPooler.Instance.Release(gameObject, gameObject.name);
        }
        else
        {
            gameObject.SetActive(false);
        }

        _prefabCreator.CreatePrefab();
        SoundsEffect.Instance.PlaySoundFxDieEnemy(this);
    }
}
