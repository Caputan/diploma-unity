using System.Collections.Generic;
using GameObjectCreating;
using UnityEngine;

namespace Diploma.Controllers
{
    public class GameContext
    {
        //public PlayerModel PlayerModel;
        public Dictionary<int,GameObjectModel> GameObjectModels;
        public Camera MainCamera;
        public GameContext()
        {
            GameObjectModels = new Dictionary<int, GameObjectModel>();
        }
        
        // public void AddPlayerModel(PlayerModel playerModel)
        // {
        //     PlayerModel = playerModel;
        // }

        public void AddCamera(Camera camera)
        {
            MainCamera = camera;
        }

        public void AddGameObjectToList(int id,GameObjectModel gameObjectModel)
        {
            GameObjectModels.Add(id,gameObjectModel);
        }
    }
}