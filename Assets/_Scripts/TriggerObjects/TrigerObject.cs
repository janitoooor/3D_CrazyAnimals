using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class TrigerObject : MonoBehaviour, ITriggerObject
{
    public void OnPlayerHealthTrigger(PlayerHealth playerHealth)
    {
        Debug.LogException(new Exception("method called from abstract class"));
    }

    private protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
            OnPlayerHealthTrigger(playerHealth);
    }
}

