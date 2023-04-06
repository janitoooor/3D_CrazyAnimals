using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmory : MonoBehaviour
{
    [SerializeField] private Gun[] _guns;
    [SerializeField] private int _currentGunIndex;

    #region Mono
    private void Start()
    {
        TakeGunByIndex(_currentGunIndex);
    }
    #endregion

    public void TakeGunByIndex(int gunIndex)
    {
        _currentGunIndex = gunIndex;

        for (int i = 0; i < _guns.Length; i++)
        {
            if (i == gunIndex)
                _guns[i].Show();
            else
                _guns[i].Hide();
        }
    }

    public void AddBullets(int gunIndex, int amountBullets)
    {
        _guns[gunIndex].AddBullets(amountBullets);
    }
}
