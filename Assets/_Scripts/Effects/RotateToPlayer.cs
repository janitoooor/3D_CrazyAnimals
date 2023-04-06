using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 _leftEuler;
    [SerializeField] private Vector3 _rightEuler;
    [Space]
    [SerializeField] private float _rotationSpeed = 5f;

    private Transform _playerTransfrom;
    private Vector3 _targetEuler;

    private void Start()
    {
        _playerTransfrom = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        if (transform.position.x < _playerTransfrom.position.x)
            _targetEuler = _rightEuler;
        else
            _targetEuler = _leftEuler;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetEuler), Time.deltaTime * _rotationSpeed);
    }
}
