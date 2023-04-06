using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageOnCollision : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private bool _dieOnAnyCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (_enemyHealth == null)
            return;

        if (collision.rigidbody && collision.rigidbody.GetComponent<Bullet>() && _enemyHealth != null)
            _enemyHealth.TakeDamage(1);

        if (_dieOnAnyCollision)
            _enemyHealth.TakeDamage(int.MaxValue);
    } 
}
