using System.IO;
using AssetBundle;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using GameObjectCreating;
using UnityEngine;

namespace Diploma.Controllers
{
    public sealed class GameObjectInitialization
    {
        private readonly string _assemblies;
        private readonly FileManager _fileManager;

        public GameObjectInitialization(string assemblies, FileManager fileManager)
        {
            _assemblies = assemblies;
            _fileManager = fileManager;
        }
        // public void Initialize()
        // {
        //     var gameObjectOnScene = _poolOfObjects.GetEnemy("GameObject");
        //     gameObjectOnScene.transform.position = new Vector3(0,0,0); ;
        //     gameObjectOnScene.gameObject.SetActive(true);
        // }

        public GameObject InstantiateGameObject()
        {
            // заменяем Loader на addressable
            // Loader3DS loader3Ds = new Loader3DS();
            // loader3Ds.StartParsing(
            //     _assemblies.Assembly_Link,
            //     _poolOfObjects._rootPool.gameObject,
            //     _poolOfObjects);
            LoadAssetBundleFromFile loadAssetBundleFromFile = new LoadAssetBundleFromFile();
            return loadAssetBundleFromFile.GetBaseObjects(_fileManager.GetStorage() + "\\" +_assemblies);
        }
    }
}