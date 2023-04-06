using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class JumpGun : MonoBehaviour
{
    [SerializeField] private Gun _pistol;
    [SerializeField] private ChargeIconUI _chargeIconUI;
    [Space]
    [SerializeField] private Rigidbody _playerRb;
    [SerializeField] private Transform _spawn;
    [Space]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxCharge = 3;

    private float _currentCharge;
    private bool _isCharged;

    private GameInput _gameInput;
    
    [Inject]
    public void Constructor(GameInput gameInput)
    {
        _gameInput = gameInput;
    }

    #region Mono
    private void Start()
    {
        _gameInput.OnPlayerJumpGunAction += GameInput_OnPlayerJumpGunAction;
    }

    private void Update()
    {
        if (!_isCharged)
            WaitChargeToJump();
    }
    #endregion

    private void GameInput_OnPlayerJumpGunAction(object sender, System.EventArgs e)
    {
        if (_isCharged)
            Jump();
    }

    private void Jump()
    {
        _playerRb.AddForce(-_spawn.forward * _speed, ForceMode.VelocityChange);
        _pistol.ShootBullet();
        _isCharged = false;
        _currentCharge = 0;
        _chargeIconUI.StartCharge();
    }

    private void WaitChargeToJump()
    {
        _currentCharge += Time.unscaledDeltaTime;
        _chargeIconUI.SetChargeValue(_currentCharge, _maxCharge);
        if (_currentCharge >= _maxCharge)
        {
            _isCharged = true;
            _chargeIconUI.StopCharge();
        }
    }
}
