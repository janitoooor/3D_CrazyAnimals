using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCenter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _followSpeed;

    private void LateUpdate()
    {
        transform.position = Vector3.Slerp(transform.position, _target.position, Time.deltaTime * _followSpeed);
    }
}
