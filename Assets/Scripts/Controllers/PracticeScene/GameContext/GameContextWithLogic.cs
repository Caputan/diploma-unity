using System.Collections.Generic;
using GameObjectCreating;
using UnityEngine;

namespace Diploma.PracticeScene.GameContext
{
    public class GameContextWithLogic
    {
        public Dictionary<int, GameObjectModel> GameObjectModels;

        public Camera MainCamera;
        public GameContextWithLogic()
        {
            GameObjectModels = new Dictionary<int, GameObjectModel>();
        }

        public void AddCamera(Camera camera)
        {
            MainCamera = camera;
        }
        
        public void AddGameObjectToList(int id, GameObjectModel gameObjectModel)
        {
            GameObjectModels.Add(id,gameObjectModel);
        }
    }
}