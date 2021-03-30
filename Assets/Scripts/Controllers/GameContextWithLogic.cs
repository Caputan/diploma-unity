using System.Collections.Generic;
using Diploma.Enums;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public sealed class GameContextWithLogic
    {
        //public PlayerModel PlayerModel;
        public Dictionary<int,GameObjectModel> GameObjectModels;
        
        public Dictionary<int, FactoryType> FactoryTypeForCreating;

        public Camera MainCamera;
        public GameContextWithLogic()
        {
            GameObjectModels = new Dictionary<int, GameObjectModel>();
            FactoryTypeForCreating = new Dictionary<int, FactoryType>();
        }

        public void AddCamera(Camera camera)
        {
            MainCamera = camera;
        }

        public void AddFactoryTypeForCreating(int id, FactoryType factoryType)
        {
            FactoryTypeForCreating.Add(id,factoryType);
        }
        public void AddGameObjectToList(int id,GameObjectModel gameObjectModel)
        {
            GameObjectModels.Add(id,gameObjectModel);
        }
    }
}