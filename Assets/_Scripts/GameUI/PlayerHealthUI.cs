using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private ObjectPool _healthIconObjectPool;
    
    private PlayerHealth _playerHealth;

    [Inject]
    public void Constructor(PlayerHealth playerHealth)
    {
        _playerHealth = playerHealth;
    }

    private void Start()
    {
        _healthIconObjectPool.Initialize();

        _playerHealth.OnPlayerChangeHealth += PlayerHealth_OnPlayerChangeHealth;
        DisplayHealth(_playerHealth.Health);
    }

    private void PlayerHealth_OnPlayerChangeHealth(object sender, System.EventArgs e)
    {
        DisplayHealth(_playerHealth.Health);
    }

    public void DisplayHealth(int currentHealth)
    {
        _healthIconObjectPool.ReleaseAll();
        for (int i = 0; i < currentHealth; i++)
        {
            GameObject objHealthIcon = _healthIconObjectPool.GetObject();
            objHealthIcon.transform.SetParent(transform, false);
        }
    }
}
