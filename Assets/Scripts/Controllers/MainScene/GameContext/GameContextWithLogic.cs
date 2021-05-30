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
        public Dictionary<int, GameObjectModel> GameObjectModels;
        
        //toggleId and LibraryTomId
        public Dictionary<int, int> FactoryTypeForCreating;

        public Camera MainCamera;
        public Camera ScreenShotCamera;
        public GameContextWithLogic()
        {
            GameObjectModels = new Dictionary<int, GameObjectModel>();
            FactoryTypeForCreating = new Dictionary<int, int>();
        }

        public void SetAScreenShotCamera(Camera camera)
        {
            ScreenShotCamera = camera;
        }
        
        public void SetAMainCamera(Camera camera)
        {
            MainCamera = camera;
        }

        public void AddFactoryTypeForCreating(int id, int LibraryTom)
        {
            FactoryTypeForCreating.Add(id,LibraryTom);
        }
        public void AddGameObjectToList(int id, GameObjectModel gameObjectModel)
        {
            GameObjectModels.Add(id,gameObjectModel);
        }
    }
}