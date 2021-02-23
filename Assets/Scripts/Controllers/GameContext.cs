using System.Collections.Generic;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class GameContext
    {
        //public PlayerModel PlayerModel;
        public Dictionary<int,GameObjectModel> GameObjectModels;
        public List<Toggle> ChoosenToggles;
        public Camera MainCamera;
        public GameContext()
        {
            GameObjectModels = new Dictionary<int, GameObjectModel>();
            ChoosenToggles = new List<Toggle>();
        }

        public void AddCamera(Camera camera)
        {
            MainCamera = camera;
        }
        public void AddToggles(Toggle toggle)
        {
            ChoosenToggles.Add(toggle);
        }
        public void AddGameObjectToList(int id,GameObjectModel gameObjectModel)
        {
            GameObjectModels.Add(id,gameObjectModel);
        }
    }
}