using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Walker : MonoBehaviour
{
    public event EventHandler OnLeftTarget;
    public event EventHandler OnRightTarget;

    [SerializeField] private Transform _leftTarget;
    [SerializeField] private Transform _rightTarget;
    [Space]
    [SerializeField] private float _speed;
    [SerializeField] private float _stopTime;
    [Space]
    [SerializeField] private Transform _rayStart;

    private bool _isStoped;

    private Direction _currentDirection;

    #region Mono

    private void Start()
    {
        _leftTarget.parent = null;
        _rightTarget.parent = null;
    }

    private void Update()
    {
        if (_isStoped)
            return;

        switch (_currentDirection)
        {
            case Direction.Left:
                WalkToRight();
                break;
            case Direction.Right:
                WalkToLeft();
                break;
        }

        if (Physics.Raycast(_rayStart.position, Vector3.down, out RaycastHit hit))
            transform.position = hit.point;
    }

    #endregion

    private void WalkToRight()
    {
        transform.position -= new Vector3(Time.deltaTime * _speed, 0f, 0f);
        bool onLeftTarget = transform.position.x < _leftTarget.position.x;

        if (onLeftTarget)
            Move(Direction.Right, OnLeftTarget);
    }

    private void WalkToLeft()
    {
        transform.position += new Vector3(Time.deltaTime * _speed, 0f, 0f);
        bool onRightTarget = transform.position.x > _rightTarget.position.x;

        if (onRightTarget)
            Move(Direction.Left, OnRightTarget);
    }

    private void Move(Direction direction, EventHandler action)
    {
        _currentDirection = direction;
        _isStoped = true;
        Invoke(nameof(ContinueWalk), _stopTime);
        action?.Invoke(this, EventArgs.Empty);
    }

    private void ContinueWalk()
    {
        _isStoped = false;
    }
}
