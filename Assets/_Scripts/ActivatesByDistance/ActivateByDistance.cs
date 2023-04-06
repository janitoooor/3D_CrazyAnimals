using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class ActivateByDistance : MonoBehaviour
{
    [SerializeField] private float _distanceToActivate = 20f;

    private readonly float _addDistanceToHide = 2f;

    private bool _isActive = true;
    private bool _isShowed;

    private Activator _activator;

    #region Mono

    private void Start()
    {
        _activator = FindObjectOfType<Activator>();
        _activator.AddObjectToList(this);
    }

    private void OnDisable()
    {
        if (_isActive)
            _activator.RemoveObjectFromList(this);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.grey;
        Handles.DrawWireDisc(transform.position, Vector3.forward, _distanceToActivate);
    }
#endif

    #endregion

    public void CheckDistance(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(transform.position, playerPosition);
        bool playerAway = distance > _distanceToActivate + _addDistanceToHide;
        bool playerClose = distance < _distanceToActivate;

        if (_isActive && playerAway)
        {
            Hide();
            return;
        }

        if (playerClose)
            Show();
    }

    public void Show()
    {
        _isActive = true;
        _isShowed = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (_isShowed)
            return;

        _isActive = false;
        gameObject.SetActive(false);
    }
}
