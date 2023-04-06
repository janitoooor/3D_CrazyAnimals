using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeDamageOnCollision : MonoBehaviour
{
    [SerializeField] private int _damageValue;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody && collision.rigidbody.TryGetComponent(out PlayerHealth playerHealth))
            playerHealth.TakeDamage(_damageValue);
    }
}
