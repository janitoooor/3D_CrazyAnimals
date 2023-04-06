using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Activator : MonoBehaviour
{
    [SerializeField] private List<ActivateByDistance> _activateByDistanceList;
    
    private Transform _playerTransform;

    [Inject]
    private void Constructor(PlayerMove playerMove)
    {
        _playerTransform = playerMove.transform;
    }

    private void Awake()
    {
        _activateByDistanceList = new List<ActivateByDistance>();
    }

    private void Update()
    {
        foreach (var item in _activateByDistanceList)
            item.CheckDistance(_playerTransform.position);
    }

    public void AddObjectToList(ActivateByDistance activateByDistanceObject)
    {
        _activateByDistanceList.Add(activateByDistanceObject);
    }

    public void RemoveObjectFromList(ActivateByDistance activateByDistanceObject)
    {
        _activateByDistanceList?.Remove(activateByDistanceObject);
    }
}
