using Diploma.Controllers.Importer;
using Diploma.Interfaces;
using Diploma.Tables;
using GameObjectCreating;
using UnityEngine;

namespace Diploma.Controllers
{
    public sealed class GameObjectInitialization: IInitialization
    {
        private readonly PoolOfObjects _poolOfObjects;
        private readonly Assemblies _assemblies;

        private readonly Material _modelMaterial;

        public FbxImporter FBXImporter;

        public GameObjectInitialization(PoolOfObjects poolOfObjects, Assemblies assemblies, Material modelMaterial)
        {
            _modelMaterial = modelMaterial;
            _poolOfObjects = poolOfObjects;
            _assemblies = assemblies;
        }
        // public void Initialize()
        // {
        //     var gameObjectOnScene = _poolOfObjects.GetEnemy("GameObject");
        //     gameObjectOnScene.transform.position = new Vector3(0,0,0); ;
        //     gameObjectOnScene.gameObject.SetActive(true);
        // }

        public void Initialization()
        {
            // физически выбросили модель на сцену
            FBXImporter = new FbxImporter();
            FBXImporter.StartParsing(
                _assemblies.Assembly_Link,
                _poolOfObjects._rootPool.gameObject,
                _poolOfObjects,
                _modelMaterial
                );
        }
    }
}