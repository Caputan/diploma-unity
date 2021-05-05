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

        public GameObject GetBaseObjects(string path)
        {
            _asset = LoadAssetBundleFromFileOnDrive(path);
            var allAssets = _asset.LoadAllAssetsAsync<GameObject>().allAssets;
            int i = 0;
            GameObject gameObject = null;
            foreach (var baseAsset in allAssets)
            {
                GameObject prefab = baseAsset as GameObject; 
                gameObject = GameObject.Instantiate(prefab,prefab.transform.position,prefab.transform.rotation);
                if (i==0)
                {
                    gameObject.transform.position = Vector3.zero;
                }
                i++;
            }
            
            _asset.Unload(false);
            return gameObject;
        }
        
    }
}