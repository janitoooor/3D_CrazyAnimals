using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RopeGun : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [Space]
    [SerializeField] private Hook _hook;
    [SerializeField] private Transform _spawn;
    [SerializeField] private Transform _ropeStart;
    [Space]
    [SerializeField] private float _speedHook;
    [SerializeField] private float _springSpringValue = 10000f;
    [SerializeField] private float _springDamperValue = 5f;
    [SerializeField] private float _distanceToDisableRope = 20f;
    [Space]
    [SerializeField] private RopeRenderer _ropeRenderer;

    private SpringJoint _springJoint;

    private float _lengthRope;

    private RopeState _currentRopeState;

    #region Mono

    private GameInput _gameInput;

    [Inject]
    public void Constructor(GameInput gameInput)
    {
        _gameInput = gameInput;
    }

    private void Start()
    {
        _gameInput.OnPlayerJumpAction += GameInput_OnPlayerJumpAction;
        _gameInput.OnMouseClickGunRopeShootAction += GameInput_OnMouseGunRopeAction;
        _hook.OnStopFixHook += Hook_OnStopFixHook;
    }

    private void Update()
    {
        switch (_currentRopeState)
        {
            case RopeState.Fly:
                InStateFly();
                break;
            case RopeState.Active:
                InStateActive();
                break;
            case RopeState.Disabled:
                InStateDisabled();
                break;
        }
    }

    #endregion

    public void DestroySpring()
    {
        if (_springJoint)
        {
            Destroy(_springJoint);
            _currentRopeState = RopeState.Disabled;
        }
    }

    public void CreateSprint()
    {
        if (!_springJoint)
        {
            _springJoint = gameObject.AddComponent<SpringJoint>();
            _springJoint.connectedBody = _hook.Rigidbody;
            _springJoint.anchor = _ropeStart.localPosition;
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = Vector3.zero;
            _springJoint.spring = _springSpringValue;
            _springJoint.damper = _springDamperValue;

            _lengthRope = Vector3.Distance(_ropeStart.position, _hook.transform.position);
            _springJoint.maxDistance = _lengthRope;

            _currentRopeState = RopeState.Active;
        }
    }

    private void GameInput_OnMouseGunRopeAction(object sender, System.EventArgs e)
    {
        ShootHook();
    }

    private void Hook_OnStopFixHook(object sender, System.EventArgs e)
    {
        _currentRopeState = RopeState.Disabled;
    }

    private void GameInput_OnPlayerJumpAction(object sender, System.EventArgs e)
    {
        if (_currentRopeState == RopeState.Active)
        {
            _playerMove.JumpOnRope();
            DestroySpring();
        }
    }

    private void InStateFly()
    {
        _ropeRenderer.DrawLine(_ropeStart.position, _hook.transform.position, _lengthRope);

        float distance = Vector3.Distance(_ropeStart.position, _hook.transform.position);

        if (distance > _distanceToDisableRope)
            _currentRopeState = RopeState.Disabled;
    }

    private void InStateDisabled()
    {
        _hook.gameObject.SetActive(false);
        _ropeRenderer.HideLine();
    }

    private void InStateActive()
    {
        _ropeRenderer.DrawLine(_ropeStart.position, _hook.transform.position, _lengthRope);
    }

    private void ShootHook()
    {
        if (_springJoint)
            Destroy(_springJoint);

        _lengthRope = 1;

        _hook.gameObject.SetActive(true);

        _hook.StopFix();

        _hook.transform.SetPositionAndRotation(_spawn.position, _spawn.rotation);

        _hook.Rigidbody.velocity = _spawn.forward * _speedHook;

        _currentRopeState = RopeState.Fly;
    }
}
