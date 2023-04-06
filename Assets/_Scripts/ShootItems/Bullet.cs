using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";

    [SerializeField] private TrailRenderer _trailRenderer;
    [SerializeField] private ObjectPool _fxObjectPool;

    #region Mono
    private void Start()
    {
        _fxObjectPool = MasterObjectPooler.Instance.GetPool(_fxObjectPool.name);
    }

    private void OnDisable()
    {
        _trailRenderer.Clear();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ReleaseBullet();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(ENEMY_TAG))
            ReleaseBullet();
    }
    #endregion

    private void ReleaseBullet()
    {
        GameObject fxObject = _fxObjectPool.GetObject();
        fxObject.transform.position = transform.position;

        if (gameObject.activeInHierarchy)
            MasterObjectPooler.Instance.Release(gameObject, name);
    }
}
