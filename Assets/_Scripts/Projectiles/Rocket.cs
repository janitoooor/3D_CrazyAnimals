using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rocket : MonoBehaviour, IProjectile
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

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
    }

    private void Update()
    {
        Move();
    }

    #endregion
    public void Move()
    {
        if (!_playerTransfrom)
        {
            MasterObjectPooler.Instance.Release(gameObject, gameObject.name);
            return;
        }

        transform.position += _speed * Time.deltaTime * transform.forward;
        Vector3 toPlayer = _playerTransfrom.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(toPlayer, Vector3.forward);
        transform.SetPositionAndRotation(
            new(transform.position.x, transform.position.y, 0),
            Quaternion.Lerp(transform.rotation, targetRotation,
            _rotationSpeed * Time.deltaTime)
            );
    }
}
