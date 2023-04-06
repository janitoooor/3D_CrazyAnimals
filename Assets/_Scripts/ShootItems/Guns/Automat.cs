using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class Automat : Gun
{
    [Header("Automat")]
    [SerializeField] private TextMeshProUGUI _bulletAmountText;
    [Space]
    [SerializeField] private int _currentAmountBullets;

    private PlayerArmory _playerArmory;
    
    [Inject]
    private void Constructor(PlayerArmory playerArmory)
    {
        _playerArmory = playerArmory;
    }

    #region Mono
    private void Start()
    {
        UpdateText();
    }
    #endregion

    public override void Hide()
    {
        base.Hide();
        _bulletAmountText.gameObject.SetActive(false);
    }

    public override void Show()
    {
        base.Show();
        _bulletAmountText.gameObject.SetActive(true);
    }

    public override void AddBullets(int amountBullets)
    {
        base.AddBullets(amountBullets);
        _currentAmountBullets += amountBullets;
        UpdateText();
        _playerArmory.TakeGunByIndex(1);
    }

    public override void ShootBullet()
    {
        base.ShootBullet();
        _currentAmountBullets--;
        UpdateText();

        if (_currentAmountBullets <= 0)
            _playerArmory.TakeGunByIndex(0);
    }

    private void UpdateText()
    {
        _bulletAmountText.text = _currentAmountBullets.ToString();
    }
}
