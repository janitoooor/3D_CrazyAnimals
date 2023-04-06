using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AdressablesLoader : MonoBehaviour
{
    public event EventHandler OnStartLoadLvl;
    public event EventHandler OnEndLoadLvl;

    [SerializeField] private AssetLabelReference _assetLabelReference;

    public void LoadAdressablesLvlPart()
    {
        OnStartLoadLvl?.Invoke(this, EventArgs.Empty);

        Addressables.LoadAssetAsync<GameObject>(_assetLabelReference).Completed +=
            (asyncOperationHandle) =>
                {
                    if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        Instantiate(asyncOperationHandle.Result);
                        OnEndLoadLvl?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                };
    }
}
