using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTargetEuler : MonoBehaviour
{
    [SerializeField] private Walker _walker;
    [SerializeField] private Vector3 _leftEuler;
    [SerializeField] private Vector3 _rightEuler;
    [Space]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private bool _isNeedRotate;

    private Vector3 _targetEuler;

    private void Start()
    {
        if (_isNeedRotate)
        {
            _walker.OnLeftTarget += Walker_OnLeftTarget;
            _walker.OnRightTarget += Walker_OnRightTarget;
        }
    }

    private void Walker_OnRightTarget(object sender, System.EventArgs e)
    {
        RotateLeft();
    }

    private void Walker_OnLeftTarget(object sender, System.EventArgs e)
    {
        RotateRight();
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetEuler), Time.deltaTime * _rotationSpeed);
    }

    public void RotateLeft()
    {
        _targetEuler = _leftEuler;
    }

    public void RotateRight()
    {
        _targetEuler = _rightEuler;
    }
}
