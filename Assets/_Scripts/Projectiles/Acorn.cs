using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Acorn : MonoBehaviour, IProjectile
{
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _maxRotationSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    public void Move()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _rigidbody.AddRelativeForce(_velocity, ForceMode.VelocityChange);
        _rigidbody.angularVelocity = new Vector3(
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed),
            Random.Range(-_maxRotationSpeed, _maxRotationSpeed)
        );
    }
}
