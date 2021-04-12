using System;
using System.Collections.Generic;
using Controllers.PracticeScene.Inventory;
using Data;
using Diploma.Interfaces;
using Diploma.Tables;
using GameObjectCreating;
using UnityEngine;

namespace Diploma.Controllers
{
    public class PracticeSceneInitialization: MonoBehaviour
    {
        [SerializeField] private ImportantDontDestroyData _data;
        
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
            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies); // 0 - assembles
            tables.Add(lessons); // 1 - lessons
            tables.Add(texts); // 2 - text
            tables.Add(types); // 3 - types
            tables.Add(users); // 4 - users
            tables.Add(videos); // 5 - videos
            
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithViews = new GameContextWithViews();
            _gameContextWithUI = new GameContextWithUI();
            
            DataBaseController.SetTable(tables[1]);
            Lessons lesson = (Lessons)DataBaseController.GetRecordFromTableById(_data.lessonID);
            DataBaseController.SetTable(tables[0]);
            Assemblies Assembly = (Assemblies)DataBaseController.GetRecordFromTableById(lesson.Lesson_Assembly_Id);
            
            var GameObjectFactory = new GameObjectFactory(true);
            var Pool = new PoolOfObjects(GameObjectFactory,_gameContextWithLogic);
            var GameObjectInitilization = new GameObjectInitialization(Pool, Assembly);
            
            var playerInitialization = new PlayerInitialization(playerPrefab, spawnPoint);

            var inventoryInitialization = new InventoryInitialization(_gameContextWithViews, _gameContextWithUI,
                mainParent, inventoryPrefab, partOfAssembly, inventorySlotPrefab);

            _controllers = new Controllers();
            _controllers.Add(playerInitialization);
            _controllers.Add(inventoryInitialization);
            _controllers.Add(GameObjectInitilization);
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