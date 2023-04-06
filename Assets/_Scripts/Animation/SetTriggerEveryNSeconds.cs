using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerEveryNSeconds : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private string _triggerName = "Attack";
    [Space]
    [SerializeField] private float _timePeriod =  7f;

    private void OnEnable()
    {
        StartCoroutine(AttackPerioud());
    }

    private IEnumerator AttackPerioud()
    {
        yield return new WaitForSeconds(_timePeriod);
        _animator.SetTrigger(_triggerName);
        StartCoroutine(AttackPerioud());
    }
}
