using System.Collections.Generic;
using Diploma.Enums;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class GameContextWithLogic
    {
        //public PlayerModel PlayerModel;
        public Dictionary<int,GameObjectModel> GameObjectModels;
        
        public Dictionary<int, FactoryType> FactoryTypes;
        public Camera MainCamera;
        public GameContextWithLogic()
        {
            GameObjectModels = new Dictionary<int, GameObjectModel>();
            FactoryTypes = new Dictionary<int, FactoryType>();
        }

        public void AddCamera(Camera camera)
        {
            MainCamera = camera;
        }

        public void AddFactoryType(int id, FactoryType factoryType)
        {
            FactoryTypes.Add(id,factoryType);
        }
        public void AddGameObjectToList(int id,GameObjectModel gameObjectModel)
        {
            GameObjectModels.Add(id,gameObjectModel);
        }
    }
}