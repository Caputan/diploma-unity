using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Interfaces;
using UnityEngine;

namespace GameObjectCreating
{
    public class GameObjectCreator: IInitialization
    {
        private readonly IFactory _factory;
        private readonly GameContextWithLogic _gameContext;
        private GameObject _gameObject;
        private GameObjectStruct _gameObjectStruct;
        private GameObjectComponents _gameObjectComponents;
        public GameObjectCreator(IFactory factory,GameContextWithLogic gameContext)
        {
            _factory = factory;
            _gameContext = gameContext;
        }
        public GameObjectProvider CreateGameObjectProvider(GameObject gameObject)
        {
            _gameObject = _factory.CreateGameObject(gameObject);
            _gameObject.AddComponent<GameObjectProvider>();

            var gameObjectStruct = (GameObjectStruct)_gameObjectStruct.Clone();
            gameObjectStruct.GameObject = _gameObject;

            var gameObjectComponents = (GameObjectComponents) _gameObjectComponents.Clone();

            var gameObjectModel = new GameObjectModel(gameObjectComponents,gameObjectStruct);
            _gameContext.AddGameObjectToList(_gameObject.GetInstanceID(),gameObjectModel);
            return _gameObject.GetComponent<GameObjectProvider>();
        }
        public void Initialization() { }
    }
}