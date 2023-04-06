using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public event EventHandler OnStopFixHook;

    [SerializeField] private Collider _hookCollider;
    [SerializeField] private Collider _playerCollider;
    [Space]
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private RopeGun _ropeGun;
    public Rigidbody Rigidbody => _rigidbody;

    private FixedJoint _fixedJoint;

    #region Mono
    private void Start()
    {
        Physics.IgnoreCollision(_hookCollider, _playerCollider);
    }

    #endregion

    private void OnCollisionEnter(Collision collision)
    {
        if (_fixedJoint)
            return;

        if (collision.rigidbody)
        {
            _fixedJoint = gameObject.AddComponent<FixedJoint>();
            _fixedJoint.connectedBody = collision.rigidbody;
            _ropeGun.CreateSprint();
        }
        else
        {
            OnStopFixHook?.Invoke(this, EventArgs.Empty);
        }
    }

    public void StopFix()
    {
        if (_fixedJoint)
            Destroy(_fixedJoint);
    }
}
