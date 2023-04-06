using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBullet : Loot
{
    [SerializeField] private int _amountBullets;
    [SerializeField] private int _gunIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out PlayerArmory playerArmory))
        {
            playerArmory.AddBullets(_gunIndex, _amountBullets);
            Die();
        }
    }
}
