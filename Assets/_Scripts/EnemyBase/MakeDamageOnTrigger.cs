using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damageValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
            playerHealth.TakeDamage(_damageValue);
    }
}
