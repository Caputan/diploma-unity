using System;
using System.Collections;
using System.IO;
using AssetBundle;
using Diploma.Enums;
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
        private LoadAssetBundleFromFile _loadAssetBundleFromFile;
        public GameObject GameObject;
        public GameObjectInitialization(string assemblies, FileManager fileManager)
        {
            _assemblies = assemblies;
            _fileManager = fileManager;
            _loadAssetBundleFromFile = new LoadAssetBundleFromFile();
            GameObject = null;
            _loadAssetBundleFromFile.LoadingIsDone += SaveSomeGameObject;
        }

        private void SaveSomeGameObject(GameObject obj)
        {
            Debug.Log("Loading is done");
            GameObject = obj;
        }

        public void InstantiateGameObject()
        {
            GameObject = null;
            _loadAssetBundleFromFile.
                    LoadAssetBundleFromFileOnDrive(_fileManager.GetStorage() + "\\" +_assemblies);
        }
    }
}