using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Hen : MonoBehaviour
{
    [SerializeField] private Rigidbody _henRigidbody;
    [Space]
    [SerializeField] private float _henSpeed = 3f;
    [SerializeField] private float _timeToReachSpeed = 1f;

    private Transform _playerTransform;

    [Inject]
    private void Constructor(PlayerMove playerMove)
    {
        _playerTransform = playerMove.transform;
    }

    #region Mono

    private void Start()
    {
        if( _playerTransform == null ) 
            _playerTransform = FindObjectOfType<PlayerMove>().transform;
    }

    private void FixedUpdate()
    {
        Vector3 topPlayer = (_playerTransform.position - transform.position).normalized;
        Vector3 force = _henRigidbody.mass * (topPlayer * _henSpeed - _henRigidbody.velocity) / _timeToReachSpeed;
        _henRigidbody.AddForce(force);
    }

    #endregion
}
