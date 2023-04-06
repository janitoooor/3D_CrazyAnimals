using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHeadRotate : MonoBehaviour
{
    [SerializeField] private Pointer _pointer;
    [SerializeField] private Transform _headTransform;
    [Space]
    [SerializeField] private float _speedRotation;
    [SerializeField] private float _maxAngleToRotate = 45f;

    public bool IsRotateToLeft { get; private set; }

    private float _yEuler;

    private void Update()
    {
        if (_pointer.DirectionToAim.x < 0)
        {
            _yEuler = Mathf.Lerp(_yEuler, _maxAngleToRotate, Time.deltaTime * _speedRotation);
            IsRotateToLeft = false;
        }
        else
        {
            _yEuler = Mathf.Lerp(_yEuler, -_maxAngleToRotate, Time.deltaTime * _speedRotation);
            IsRotateToLeft = true;
        }

        _headTransform.localEulerAngles = new Vector3(0f, _yEuler, 0f);
    }
}


