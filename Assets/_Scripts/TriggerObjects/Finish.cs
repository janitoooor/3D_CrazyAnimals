using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour, ITriggerObject
{
    public void OnPlayerHealthTrigger(PlayerHealth playerHealth)
    {
        playerHealth.TakeFinish();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
            OnPlayerHealthTrigger(playerHealth);
    }
}
