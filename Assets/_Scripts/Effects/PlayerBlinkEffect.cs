using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerBlinkEffect : BlinkEffect
{
    private PlayerHealth _playerHealth;

    [Inject]
    public void Constructor(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Start()
    {
        _playerHealth.OnPlayerTakeDamage += PlayerHealth_OnPlayerTakeDamage;
    }

    private void PlayerHealth_OnPlayerTakeDamage(object sender, System.EventArgs e)
    {
        StartBlink();
    }
}
