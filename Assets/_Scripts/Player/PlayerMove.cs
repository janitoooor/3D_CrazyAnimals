using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerMove : MonoBehaviour
{
    public event EventHandler OnPlayerSitDown;
    public event EventHandler OnPlayerUnSitDown;

    [SerializeField] private PlayerHeadRotate _playerHeadRotate;
    [Space]
    [SerializeField] private ParticleSystem _playerMovingFx;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _colliderTransform;
    [Space]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _speedMultiplierInJump;
    [SerializeField] private float _friction;
    [SerializeField] private float _maxAngleToJump;
    [SerializeField] private float _heightOnSitDown;
    [SerializeField] private float _sitDownSpeed;
    [SerializeField] private float _rotationSpeedOnJump = 10f;

    private readonly int _jumpFramesToJump = 2;

    private readonly float _speedToNomralizeRotation = 20f;

    private int _jumpFrameCounter;

    private float _normalHeight;

    private bool _isGrounded = true;
    private bool _isSitDown;
    
    private GameInput _gameInput;

    [Inject]
    private void Constructor(GameInput gameInput)
    {
        _gameInput = gameInput;
    }

    #region Mono

    [Obsolete]
    private void Start()
    {
        _gameInput.OnPlayerJumpAction += GameInput_OnPlayerJumpAction;
        _gameInput.OnPlayerSitDownAction += GameInput_OnPlayerSitDownAction;
        _gameInput.OnPlayerUnSitDownAction += GameInput_OnPlayerUnSitDownAction;

        _normalHeight = transform.localScale.y;
        _playerMovingFx.enableEmission = false;
    }

    private void Update()
    {
        if (_isSitDown || !_isGrounded)
            SitDown();
        else
            UnSitDown();
    }

    private void FixedUpdate()
    {
        float speedMultiplier = 1;

        if (!_isGrounded)
        {
            speedMultiplier = _speedMultiplierInJump;
            _jumpFrameCounter++;

            if (IsMaxVelocity())
                speedMultiplier = 0;

            RotateOnJump();
        }
        else
        {
            _rigidbody.AddForce(-_rigidbody.velocity.x * _friction, 0, 0, ForceMode.VelocityChange);
            StopRotateOnJump();
        }

        _rigidbody.AddForce(_gameInput.GetMovementVectorNormalized().x * _moveSpeed * speedMultiplier, 0, 0, ForceMode.VelocityChange);
    }

    [Obsolete]
    private void OnCollisionStay(Collision collision)
    {
        foreach (var item in collision.contacts)
        {
            float angle = Vector3.Angle(item.normal, Vector3.up);
            if (angle < _maxAngleToJump)
            {
                _isGrounded = true;
                _playerMovingFx.enableEmission = true;
            }
        }
    }

    [Obsolete]
    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
        _playerMovingFx.enableEmission = false;
    }
    #endregion

    public void JumpOnRope()
    {
        if (!_isGrounded)
            Jump();
    }

    private void JumpOnGround()
    {
        if (_isGrounded)
            Jump();
    }

    private void StopRotateOnJump()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.fixedDeltaTime * _speedToNomralizeRotation);
        _rigidbody.freezeRotation = true;
    }

    private void RotateOnJump()
    {
        if (_jumpFrameCounter != _jumpFramesToJump)
            return;

        _rigidbody.freezeRotation = false;
        if (_playerHeadRotate.IsRotateToLeft)
            _rigidbody.AddRelativeTorque(0, 0, -_rotationSpeedOnJump, ForceMode.VelocityChange);
        else
            _rigidbody.AddRelativeTorque(0, 0, _rotationSpeedOnJump, ForceMode.VelocityChange);
    }

    private void Jump()
    {
        _rigidbody.AddForce(0, _jumpSpeed, 0, ForceMode.VelocityChange);
        _jumpFrameCounter = 0;
    }

    private bool IsMaxVelocity()
    {
        return _rigidbody.velocity.x > _maxSpeed
            || _rigidbody.velocity.x < -_maxSpeed;
    }

    private void GameInput_OnPlayerJumpAction(object sender, System.EventArgs e)
    {
        JumpOnGround();
    }

    private void GameInput_OnPlayerUnSitDownAction(object sender, System.EventArgs e)
    {
        _isSitDown = false;
    }

    private void GameInput_OnPlayerSitDownAction(object sender, System.EventArgs e)
    {
        _isSitDown = true;
    }

    private void SitDown()
    {
        ChangeHeight(_heightOnSitDown);
        OnPlayerSitDown?.Invoke(this, EventArgs.Empty);
    }

    private void UnSitDown()
    {
        ChangeHeight(_normalHeight);
        OnPlayerUnSitDown?.Invoke(this, EventArgs.Empty);
    }

    private void ChangeHeight(float height)
    {
        Vector3 newScale = new(
            _colliderTransform.localScale.x,
            height,
            _colliderTransform.localScale.z);

        _colliderTransform.localScale = Vector3.Lerp(_colliderTransform.localScale, newScale, Time.deltaTime * _sitDownSpeed);
    }

}
