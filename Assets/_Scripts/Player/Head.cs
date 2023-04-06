using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Pointer _pointer;

    private void Start()
    {
        _playerMove.OnPlayerSitDown += PlayerMove_OnPlayerSitDown;
        _playerMove.OnPlayerUnSitDown += PlayerMove_OnPlayerUnSitDown;
    }

    private void PlayerMove_OnPlayerUnSitDown(object sender, System.EventArgs e)
    {
        transform.position = _target.position;
    }

    private void PlayerMove_OnPlayerSitDown(object sender, System.EventArgs e)
    {
        transform.position = _target.position;
    }
}
