using UnityEngine;


namespace AssetBundle
{
    public sealed class LoadAssetBundleFromFile
    {
        private UnityEngine.AssetBundle _asset;
        private UnityEngine.AssetBundle LoadAssetBundleFromFileOnDrive(string path)
        {
           return UnityEngine.AssetBundle.LoadFromFileAsync(path).assetBundle;
        }

        public void GetBaseObjects(string path)
        {
            _asset = LoadAssetBundleFromFileOnDrive(path);
            var allAssets = _asset.LoadAllAssetsAsync<GameObject>().allAssets;
            foreach (var baseAsset in allAssets)
            {
                GameObject prefab = baseAsset as GameObject;
                GameObject.Instantiate(prefab,prefab.transform.position,prefab.transform.rotation);
            }
            _asset.Unload(false);
        }
        
    }
}