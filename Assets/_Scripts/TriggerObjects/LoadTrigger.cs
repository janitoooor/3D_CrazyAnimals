using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LoadTrigger : MonoBehaviour, ITriggerObject
{
    private AdressablesLoader _adressablesLoader;

    [Inject]
    public void Constructor(AdressablesLoader adressablesLoader)
    {
        _adressablesLoader = adressablesLoader;
    }

    public void OnPlayerHealthTrigger(PlayerHealth playerHealth)
    {
        _adressablesLoader.LoadAdressablesLvlPart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out PlayerHealth playerHealth))
            OnPlayerHealthTrigger(playerHealth);
    }
}
