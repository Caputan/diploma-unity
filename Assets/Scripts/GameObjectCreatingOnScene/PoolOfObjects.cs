using System;
using System.Collections.Generic;
using System.Linq;
using Diploma.Controllers;
using Managers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameObjectCreating
{
    public class PoolOfObjects
    {
        private readonly Dictionary<int,GameObjectProvider> _gameObjectPool;
       
        private int _count;
        public Transform _rootPool;
        private GameObjectCreator _gameObjectCreator;
        public PoolOfObjects(GameObjectFactory gameObjectFactory,GameContextWithLogic gameContext)
        {
            _gameObjectCreator = new GameObjectCreator(gameObjectFactory,gameContext);
            _gameObjectPool = new Dictionary<int,GameObjectProvider>();
            _count = 0;
            
            if (!_rootPool)
            {
                _rootPool = new
                    GameObject(PoolManager.POOL_OBJECTS).transform;
            }
        }
        
        public GameObjectProvider GetElement(int idOfElement)
        {
            return _gameObjectPool[idOfElement];
        }

        public void AddInfoInPool(GameObject gameObject)
        {
           var gameObjectProvider = _gameObjectCreator.CreateGameObjectProvider(gameObject);
           _gameObjectPool.Add(gameObject.GetInstanceID(),gameObjectProvider);
        }
        // private GameObjectProvider GetGameObject(int id)
        // {
        //     //переписать это для взятие конкретного объекта
        //     var enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
        //     if (enemy == null )
        //     {
        //         
        //         // for (var i = 0; i < _capacityPool; i++)
        //         // {
        //         //     var instantiate = _gameObjectCreator.CreateGameObjectProvider(_count);
        //         //     ReturnToPool(instantiate.transform);
        //         //     enemies.Add(instantiate);
        //         //     _count++;
        //         // }
        //         
        //         GetGameObject(enemies);
        //         
        //     }
        //     enemy = enemies.FirstOrDefault(a => !a.gameObject.activeSelf);
        //     return enemy;
        // }

        // public List<GameObjectProvider> GetAllGameObjects()
        // {
        //     GetEnemy("GameObject");
        //     List<GameObjectProvider> listOfAsteroidProviders  = _gameObjectPool["GameObject"].ToList();
        //     return listOfAsteroidProviders;
        // }

        public void RemovePool()
        {
            Object.Destroy(_rootPool);
        }
    }
}