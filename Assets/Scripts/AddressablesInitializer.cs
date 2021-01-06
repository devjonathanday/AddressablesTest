using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class AddressablesInitializer : MonoBehaviour
{
    public static AddressablesInitializer instance;
    public TextMeshProUGUI statusText;
    public List<string> assetKeys;

    private void Awake()
    {
        instance = this;
    }
    
    //Initialization

    public void InitializeAddressables()
    {
        var handle = Addressables.InitializeAsync();
        handle.Completed += LogInitializeSuccess;
    }

    private void LogInitializeSuccess(AsyncOperationHandle<IResourceLocator> obj)
    {
        statusText.text = "Initialized!";
    }
    
    //Calculate download size

    public void CalculateDownloadSize()
    {
        var handle = Addressables.GetDownloadSizeAsync(assetKeys.ToArray());
        handle.Completed += OnCalcSizeCompleted;
    }

    private void OnCalcSizeCompleted(AsyncOperationHandle<long> size)
    {
        statusText.text = $"Asset Download Size: {size.Result} bytes";
    }
    
    //Asset downloading

    public void DownloadAssets()
    {
        var handle = Addressables.DownloadDependenciesAsync(assetKeys.ToArray(), Addressables.MergeMode.None, true);
        handle.Completed += LogCompletion;
    }

    private void LogCompletion(AsyncOperationHandle obj)
    {
        statusText.text = "Finished Downloading Assets!";
    }
}