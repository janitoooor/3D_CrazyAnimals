using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{
    public event EventHandler OnPlayerTakeDamage;
    public event EventHandler OnPlayerChangeHealth;
    public event EventHandler OnPlayerDie;
    public event EventHandler OnPlayerTakeFinish;

    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    [SerializeField] private float _timeInvulnerable = 1f;

    public int Health => _health;

    private bool _invulnerable = false;

    public void TakeFinish()
    {
        OnPlayerTakeFinish?.Invoke(this, EventArgs.Empty);
    }

    public void TakeDamage(int damageValue)
    {
        if (_invulnerable)
            return;

        OnPlayerTakeDamage?.Invoke(this, EventArgs.Empty);
        PlayerEffectsOnTakeDamage();

        _health -= damageValue;

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }

        OnPlayerChangeHealth?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerEffectsOnTakeDamage()
    {
        _invulnerable = true;
        Invoke(nameof(StopInvulnerable), _timeInvulnerable);
    }

    private void StopInvulnerable()
    {
        _invulnerable = false;
    }

    public void AddHealth(int health)
    {
        _health += health;

        if (_health > _maxHealth)
            _health = _maxHealth;

        OnPlayerChangeHealth?.Invoke(this, EventArgs.Empty);
    }

    private void Die()
    {
        OnPlayerDie?.Invoke(this, EventArgs.Empty);
    }
}
