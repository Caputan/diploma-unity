
using System.Collections.Generic;
using Controllers.PracticeScene.UIController;
using Data;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.PracticeScene.GameContext;
using Diploma.Tables;
using UnityEngine;
using GameContextWithLogic = Diploma.PracticeScene.GameContext.GameContextWithLogic;
using GameContextWithUI = Diploma.PracticeScene.GameContext.GameContextWithUI;
using UIController = Controllers.PracticeScene.UIController.UIController;

namespace Diploma.PracticeScene.Controllers
{
    public class PracticeSceneInitialization: MonoBehaviour
    {
        [SerializeField] private ImportantDontDestroyData _data;
        
        [SerializeField] private GameObject mainParent;

        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnPoint;

        [SerializeField] private GameObject inventoryPrefab;
        [SerializeField] private GameObject inventorySlotPrefab;

        [SerializeField] private GameObject basePart;
        [SerializeField] private GameObject[] partOfAssembly;

        private GameContextWithView _gameContextView;
        private GameContextWithUI _gameContextWithUI;
        private GameContextWithLogic _gameContextWithLogic;
        
        private Diploma.Controllers.Controllers _controllers;

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

            _gameContextView = new GameContextWithView();
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithUI = new GameContextWithUI();
            
            DataBaseController.SetTable(tables[1]);
            Lessons lesson = (Lessons)DataBaseController.GetRecordFromTableById(_data.lessonID);
            DataBaseController.SetTable(tables[0]);
            Assemblies assembly = (Assemblies)DataBaseController.GetRecordFromTableById(lesson.Lesson_Assembly_Id);
            
            //var GameObjectFactory = new GameObjectFactory();
            //var Pool = new PoolOfObjects(GameObjectFactory, _gameContextWithLogic);
            var GameObjectInitialization = new GameObjectInitialization(assembly);

            var playerInitialization = new PlayerInitialization(playerPrefab, spawnPoint);

            //var inventoryInitialization = new InventoryInitialization(_gameContextWithViews, _gameContextWithUI,
               // mainParent, inventoryPrefab, partOfAssembly, inventorySlotPrefab);

           // var assemblyInitialization = new AssemblyInitialization(basePart, partOfAssembly);


            var uiController = new UIController(_gameContextWithUI);
           
            _controllers = new Diploma.Controllers.Controllers();
            _controllers.Add(playerInitialization);
            _controllers.Add(GameObjectInitialization);
            //_controllers.Add(inventoryInitialization);
            //_controllers.Add(assemblyInitialization);
            
            //
            _controllers.Add(uiController);
            _controllers.Initialization();
        }

        private void FixedUpdate()
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