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

        public GameObject InstantiateGameObject()
        {
            LoadAssetBundleFromFile loadAssetBundleFromFile = new LoadAssetBundleFromFile();
            return loadAssetBundleFromFile.GetBaseObjects(_fileManager.GetStorage() + "\\" +_assemblies);
        }
    }
}