using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Carrot : MonoBehaviour, IProjectile
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _speedCarrot;

    private Transform _playerTransfrom;

    [Inject]
    private void Constructor(PlayerMove playerMove)
    {
        _playerTransfrom = playerMove.transform;
    }

    #region Mono

    private void OnEnable()
    {
        if (_playerTransfrom == null)
            _playerTransfrom = FindObjectOfType<PlayerMove>().transform;

        if (_playerTransfrom == null)
        {
            print("PlayerTransform is null");
            return;
        }

        Vector3 toPlayer = (_playerTransfrom.position - transform.position).normalized;
        _rigidBody.velocity = toPlayer * _speedCarrot;
    }

    private void Update()
    {
        Move();
    }

    #endregion

    public void Move()
    {
        transform.SetPositionAndRotation(new(transform.position.x, transform.position.y, 0), Quaternion.identity);
    }
}
