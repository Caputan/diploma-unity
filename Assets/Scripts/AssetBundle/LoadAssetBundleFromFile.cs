using System;
using System.Collections;
using Coroutine;
using Diploma.Extensions;
using UnityEngine;


namespace AssetBundle
{
    public sealed class LoadAssetBundleFromFile
    {
        private UnityEngine.AssetBundle _asset;
        public event Action<GameObject> LoadingIsDone;
        public void LoadAssetBundleFromFileOnDrive(string path)//,
           // out string WaitIsLoading,out float progress,out GameObject gameObject)
        {
            LoadingWaiting(path).StartCoroutine(out _, out _);
        }

        private IEnumerator LoadingWaiting(string path)
        {
            yield return new WaitForFixedUpdate();
            AssetBundleCreateRequest assetBundle = UnityEngine.AssetBundle.LoadFromFileAsync(path);
            yield return new WaitUntil(() => assetBundle.isDone);
            LoadingIsDone.Invoke(GetBaseObjects(assetBundle.assetBundle));
            yield return new WaitForEndOfFrame();
            yield break;
        }
        
        private GameObject GetBaseObjects(UnityEngine.AssetBundle assetBundle)
        {
            _asset = assetBundle;
            _asset.GetAllAssetNames();
            var allAssets = _asset.LoadAllAssetsAsync<GameObject>().allAssets;
            GameObject gameObject = null;
            foreach (var baseAsset in allAssets)
            {
                gameObject = baseAsset as GameObject;
            }
            _asset.Unload(false);
            return gameObject;
        }
    }
}