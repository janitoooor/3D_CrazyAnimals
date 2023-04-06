using QFSW.MOP2;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class AcornCreator : MonoBehaviour, ICreator
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private Transform[] _spawnPos;
    public void CreateAcorns()
    {
        foreach (Transform item in _spawnPos)
            CreatePrefab(item);
    }

    public void CreatePrefab(Transform itemTransform)
    {
        Acorn acorn = _objectPool.GetObjectComponent<Acorn>(itemTransform.transform.position, itemTransform.transform.rotation);
        acorn.Move();
    }
}
