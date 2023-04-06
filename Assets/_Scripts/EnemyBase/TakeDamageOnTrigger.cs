using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageOnTrigger : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private bool _dieOnAnyCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (_enemyHealth == null)
            return;

        if (other.attachedRigidbody && other.attachedRigidbody.GetComponent<Bullet>() && _enemyHealth != null)
            _enemyHealth.TakeDamage(1);

        if (_dieOnAnyCollision && !other.isTrigger)
            _enemyHealth.TakeDamage(int.MaxValue);
    }
}
