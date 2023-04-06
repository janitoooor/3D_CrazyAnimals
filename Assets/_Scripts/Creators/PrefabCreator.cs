using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PrefabCreator : MonoBehaviour, ICreator
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private Transform _spawnTransform;

    public void CreatePrefab(Transform itemTransform = null)
    {
        MasterObjectPooler.Instance.GetObject(_objectPool.name, _spawnTransform.position, _spawnTransform.rotation);
    }
}
