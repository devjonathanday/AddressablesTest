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
    public TextMeshProUGUI sizeText;
    public List<string> assetKeys;
    public TextMeshProUGUI errorLog;

    private void Awake()
    {
        instance = this;
    }

    public void InitializeAddressables()
    {
        try
        {

            var handle = Addressables.InitializeAsync();
            handle.Completed += CalculateDownloadSize;
        }
        catch (Exception e)
        {
            errorLog.text += "\n" + e;
        }
    }

    private void CalculateDownloadSize(AsyncOperationHandle<IResourceLocator> obj)
    {
        try
        {
            var handle = Addressables.GetDownloadSizeAsync(assetKeys.ToArray());
            handle.Completed += OnCalcSizeCompleted;
        }
        catch (Exception e)
        {
            errorLog.text += "\n" + e;
        }
    }

    public void OnCalcSizeCompleted(AsyncOperationHandle<long> size)
    {
        sizeText.text = $"Asset Download Size: {size.Result} bytes";
    }

    public void DownloadAssets()
    {
        try
        {
            var handle = Addressables.DownloadDependenciesAsync(assetKeys.ToArray(), Addressables.MergeMode.None, true);
            handle.Completed += LogCompletion;
        }
        catch (Exception e)
        {
            errorLog.text += "\n" + e;
        }
    }

    private void LogCompletion(AsyncOperationHandle obj)
    {
        sizeText.text = "Finished Downloading Assets!";
    }
}