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

        public GameObjectInitialization(PoolOfObjects poolOfObjects,Assemblies assemblies)
        {
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
            // заменяем Loader на addressable
            Loader3DS loader3Ds = new Loader3DS();
            loader3Ds.StartParsing(
                _assemblies.Assembly_Link,
                _poolOfObjects._rootPool.gameObject,
                _poolOfObjects);
            
        }
    }
}