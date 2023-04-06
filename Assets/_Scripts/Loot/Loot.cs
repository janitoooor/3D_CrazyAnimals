using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public abstract class Loot : MonoBehaviour
{
    [SerializeField] private PrefabCreator _prefabCreator;

    private protected virtual void Die()
    {
        SoundsEffect.Instance.PlaySoundTakeLoot(this);
        _prefabCreator.CreatePrefab();
        gameObject.SetActive(false);
    }
}
