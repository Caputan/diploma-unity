
using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.PracticeScene.PauseController;
using Data;
using Diploma.Controllers;
using Diploma.Controllers.AssembleController;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.PracticeScene.GameContext;
using Diploma.Tables;
using UI.LoadingUI;
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
        
        [SerializeField] private GameObject completePrefab;
        
        [SerializeField] private Transform assemblyParent;
        [SerializeField] private GameObject[] partOfAssembly;

        private GameContextWithView _gameContextView;
        private GameContextWithUI _gameContextWithUI;
        private PlayerInitialization _playerInitialization;
        private PracticeSceneController _practiceSceneController;
        private GameObjectInitialization _gameObjectInitialization;
        private UIController _uiController;
        private AssemblyInitialization _assemblyInitialization;
        private LoadingUILogic _loadingUILogic;
        private Diploma.Controllers.Controllers _controllers;
        
        private IEnumerator Start()
        {
            Debug.Log("START");
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
            _gameContextWithUI = new GameContextWithUI();
            
            DataBaseController.SetTable(tables[1]);
            Lessons lesson = (Lessons)DataBaseController.GetRecordFromTableById(_data.lessonID);
            DataBaseController.SetTable(tables[0]);
            Assemblies assembly = (Assemblies)DataBaseController.GetRecordFromTableById(lesson.Lesson_Assembly_Id);
            
            _loadingUILogic = new LoadingUILogic(mainParent.transform);
            _loadingUILogic.Initialization();
            var fileManager = new FileManager();
            
            yield return new WaitForFixedUpdate();
            _loadingUILogic.SetActiveLoading(true);
            _loadingUILogic.LoadingParams("Ожидайте,пожалуйста","Загрузка");
            _gameObjectInitialization = new GameObjectInitialization(assembly.Assembly_Link, fileManager);
            _gameObjectInitialization.InstantiateGameObject();
            yield return new WaitUntil(()=> _gameObjectInitialization.GameObject != null);

            _practiceSceneController = new PracticeSceneController(DataBaseController, tables, _data,
                new LoadingSceneController(), completePrefab, mainParent.transform, _gameContextView);
            
            var assemblyGameObject = _gameObjectInitialization.GameObject;
            
            _playerInitialization = new PlayerInitialization(playerPrefab, spawnPoint, _data);
            
            _assemblyInitialization = new AssemblyInitialization(assemblyGameObject, lesson.Lesson_Assembly_Order, assemblyParent);
            yield return new WaitForFixedUpdate();
            _loadingUILogic.SetActiveLoading(false);
            
            var pauseInitialization = new PauseInitialization(_gameContextView,_gameContextWithUI,mainParent);
            var pauseController = new PauseController(_data,new LoadingSceneController());
            pauseController.SetAnPracticeScene(this);
            var ExitController = new ExitController(_data);
            _uiController = new UIController(_gameContextWithUI,pauseController,_playerInitialization);

            _controllers = new Diploma.Controllers.Controllers();
            _controllers.Add(_playerInitialization);
            _controllers.Add(pauseInitialization);
            _controllers.Add(ExitController);
            _controllers.Add(_assemblyInitialization);
            _controllers.Add(_practiceSceneController);
            _controllers.Add(_uiController);
            _controllers.Initialization();
            yield break;
        }
        
        public void DecompileGameScene()
        {
            //мы должны удалить игрока, удалить объект и вызвать start
            
            DestroyImmediate(_playerInitialization.playerGO);
            DestroyImmediate(_gameObjectInitialization.GameObject,true);
            DestroyImmediate(_assemblyInitialization.GetAGameObject());
            DestroyImmediate(_loadingUILogic._settingActiveGameObject);
            _uiController.ActivatePauseMenu(false);
            _controllers.CleanData();
            _controllers = null;
            StartCoroutine(Start());
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers?.Execute(deltaTime);
        }
    }
}