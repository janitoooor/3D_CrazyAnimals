using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerTakeDamages : MonoBehaviour
{
    [SerializeField] private Animator _bearAnimator;
    [SerializeField] private EnemyHealth _enemyHealth;
    [Space]
    [SerializeField] private string _triggerName;

    private void Start()
    {
        _enemyHealth.OnEnemyTakeDamage.AddListener(() =>
        {
            _bearAnimator.SetTrigger(_triggerName);
        });
    }
}
