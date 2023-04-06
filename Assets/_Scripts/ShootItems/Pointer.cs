using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Pointer : MonoBehaviour
{
    [SerializeField] private Transform _aim;
    [SerializeField] private Camera _playerCamera;

    public Vector3 DirectionToAim { get; private set; }

    private GameInput _gameInput;

    [Inject]
    public void Constructor(GameInput gameInput)
    {
        _gameInput = gameInput;
    }

    private void LateUpdate()
    {
        Ray ray = _playerCamera.ScreenPointToRay(_gameInput.GetMouseVectorNormalized());
        Debug.DrawRay(ray.origin, ray.direction * 50);

        Plane plane = new(-Vector3.forward, Vector3.zero);
        plane.Raycast(ray, out float distance);
        _aim.transform.position = ray.GetPoint(distance);

        DirectionToAim = _aim.position - transform.position;
        transform.rotation = Quaternion.LookRotation(DirectionToAim);
    }
}
