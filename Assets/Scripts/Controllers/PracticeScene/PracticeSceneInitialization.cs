using System;
using Controllers.PracticeScene.Inventory;
using UnityEngine;

namespace Diploma.Controllers
{
    public class PracticeSceneInitialization: MonoBehaviour
    {
        [SerializeField] private GameObject mainParent;

        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private GameObject inventoryPrefab;
        [SerializeField] private GameObject inventorySlotPrefab;
        
        [SerializeField] private GameObject[] partOfAssembly;

        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private GameContextWithLessons _gameContextWithLessons;
        private GameContextWithUI _gameContextWithUI;

        private Controllers _controllers;

        public void Start()
        {
            var playerInitialization = new PlayerInitialization(playerPrefab, spawnPoint);

            var inventoryInitialization = new InventoryInitialization(_gameContextWithViews, _gameContextWithUI,
                mainParent, inventoryPrefab, partOfAssembly, inventorySlotPrefab);

            _controllers = new Controllers();
            _controllers.Add(playerInitialization);
            _controllers.Add(inventoryInitialization);
            _controllers.Initialization();
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }
        
        // private void LateUpdate()
        // {
        //     var deltaTime = Time.deltaTime;
        //     _controllers.LateExecute(deltaTime);
        // }
        //
        // private void OnDestroy()
        // {
        //     _controllers.CleanData();
        // }
    }
}