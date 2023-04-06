using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnPlayerJumpAction;

    public event EventHandler OnPlayerJumpGunAction;

    public event EventHandler OnPlayerSitDownAction;
    public event EventHandler OnPlayerUnSitDownAction;

    public event EventHandler OnMouseClickShotAction;
    public event EventHandler OnMouseUnClickShotAction;

    public event EventHandler OnMouseClickTimeSlowAction;
    public event EventHandler OnMouseUnClickTimeSlowAction;
    
    public event EventHandler OnMouseClickGunRopeShootAction;

    private PlayerAction _playerAction;

    #region Mono
    private void OnEnable()
    {
        _playerAction = new PlayerAction();
        _playerAction.Enable();

        _playerAction.Player.Jump.performed += Jump_performed;

        _playerAction.Player.JumpGun.performed += JumpGun_performed;
        
        _playerAction.Player.SitDown.started += SitDown_started;
        _playerAction.Player.SitDown.canceled += SitDown_canceled;

        _playerAction.Mouse.Shoot.started += Shoot_started;
        _playerAction.Mouse.Shoot.canceled += Shoot_canceled;

        _playerAction.Mouse.TimeSlow.started += TimeSlow_started;
        _playerAction.Mouse.TimeSlow.canceled += TimeSlow_canceled;

        _playerAction.Mouse.GunRopeShoot.performed += GunRopeShoot_performed;
    }

    private void GunRopeShoot_performed(InputAction.CallbackContext obj)
    {
        OnMouseClickGunRopeShootAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable()
    {
        _playerAction.Player.Jump.performed -= Jump_performed;

        _playerAction.Player.JumpGun.performed -= JumpGun_performed;

        _playerAction.Player.SitDown.started -= SitDown_started;
        _playerAction.Player.SitDown.canceled -= SitDown_canceled;

        _playerAction.Mouse.Shoot.started -= Shoot_started;
        _playerAction.Mouse.Shoot.canceled -= Shoot_canceled;

        _playerAction.Mouse.TimeSlow.started -= TimeSlow_started;
        _playerAction.Mouse.TimeSlow.canceled -= TimeSlow_canceled;

        _playerAction.Mouse.GunRopeShoot.performed -= GunRopeShoot_performed;

        _playerAction.Dispose();
    }

    #endregion

    private void JumpGun_performed(InputAction.CallbackContext obj)
    {
        OnPlayerJumpGunAction?.Invoke(this, EventArgs.Empty);
    }

    private void TimeSlow_canceled(InputAction.CallbackContext obj)
    {
        OnMouseUnClickTimeSlowAction?.Invoke(this, EventArgs.Empty);
    }

    private void TimeSlow_started(InputAction.CallbackContext obj)
    {
        OnMouseClickTimeSlowAction.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_canceled(InputAction.CallbackContext obj)
    {
        OnMouseUnClickShotAction?.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_started(InputAction.CallbackContext obj)
    {
        OnMouseClickShotAction?.Invoke(this, EventArgs.Empty);
    }

    private void SitDown_canceled(InputAction.CallbackContext obj)
    {
        OnPlayerUnSitDownAction?.Invoke(this, EventArgs.Empty);
    }

    private void SitDown_started(InputAction.CallbackContext obj)
    {
        OnPlayerSitDownAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        OnPlayerJumpAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return _playerAction.Player.Move.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetMouseVectorNormalized()
    {
        return _playerAction.Mouse.Position.ReadValue<Vector2>();
    }
}
