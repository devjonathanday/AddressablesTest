using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class GenerateObject : MonoBehaviour
{
    public TMP_InputField inputField;

    public int position = 0;
    public int index = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(AddressablesInitializer.instance.assetKeys[index]);
            handle.Completed += AssetFinishedLoading;
        }
    }

    void AssetFinishedLoading(AsyncOperationHandle<GameObject> obj)
    {
        if(obj.Status != AsyncOperationStatus.Failed)
        {
            var objToSpawn = obj.Result;
            Instantiate(objToSpawn, Vector3.zero + (Vector3.right * position), Quaternion.identity);
            position++;
            index++;
            if (index >= AddressablesInitializer.instance.assetKeys.Count)
            {
                index = 0;
            }
        }
    }
}