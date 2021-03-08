using Diploma.Interfaces;
using GameObjectCreating;
using UnityEngine;

namespace Diploma.Controllers
{
    public sealed class GameObjectInitialization: IInitialization
    {
        private readonly PoolOfObjects _poolOfObjects;

        public GameObjectInitialization(PoolOfObjects poolOfObjects)
        {
            _poolOfObjects = poolOfObjects;
        }
        public void Initialize()
        {
            var gameObjectOnScene = _poolOfObjects.GetEnemy("GameObject");
            gameObjectOnScene.transform.position = new Vector3(Random.Range(-10f,10f),Random.Range(-10f,10f),0); ;
            gameObjectOnScene.gameObject.SetActive(true);
        }
        public void Initialization() { }
    }
}