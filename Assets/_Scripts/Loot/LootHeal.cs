using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootHeal : Loot
{
    [SerializeField] private int _healthValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.AddHealth(_healthValue);
            Die();
        }
    }
}
