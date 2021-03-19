using System;
using System.Collections.Generic;
using Controllers;
using Diploma.Constructor;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using Diploma.UI;
using Diploma.Managers;
using GameObjectCreating;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Types = Diploma.Tables.Types;


namespace Diploma.Controllers
{
    public sealed class ControleerInitializer : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int countOfDetails;
        [SerializeField] private GameObject togglePanelPrefab;
        [SerializeField] private GameObject ToggleGroup;

        [SerializeField] private GameObject toggleLessonPrefab;
        [SerializeField] private GameObject ParentForLessons;
        
        [SerializeField] private FileManagerController _fileManagerController;
        [SerializeField] private Loader3DS _loader3Ds;
        
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private GameContextWithLessons _gameContextWithLessons;
        
        private Controllers _controllers;
        public string[] destinationPath = new string[4];
        private void Start()
        {
            

            #region DataBase initialization

            var fileManager = new FileManager();
            destinationPath[0] = fileManager.CreateFileFolder("Assemblies");
            destinationPath[1] = fileManager.CreateFileFolder("Videos");
            destinationPath[2] = fileManager.CreateFileFolder("Photos");
            destinationPath[3] = fileManager.CreateFileFolder("Texts");
            
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

            #endregion

            #region Authentication

            // var AuthController = new AuthController(DataBaseController, tables, );

            #endregion
            
            #region Creation UI and GameContext
            
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithViews = new GameContextWithViews();
            _gameContextWithLessons = new GameContextWithLessons();
            
            
            

            //var uiController = new UIController();
            // добавить соответствующие менюшки ниже
            // с помощью uiController.AddUIToDictionary()
            
            // тут мы создали базове типизированное меню
            // var GameContextWithViewCreator = new GameContexWithViewCreator(
            //     _gameContextWithViews,
            //     _gameContextWithLogic,
            //     _gameContextWithLessons,
            //     ToggleGroup,
            //     togglePanelPrefab,
            //     ParentForLessons,
            //     toggleLessonPrefab,
            //     DataBaseController,
            //     tables
            //     );
            
            #endregion


            _fileManagerController = new FileManagerController
            {
                DataBaseController = DataBaseController, Tables = tables, destinationPath = destinationPath
            };

            #region Creation new Lession Module
            // данный регион будет вызываться во время создания урока
            var abstractFactory = new AbstractFactory();
            var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic,_button,_fileManagerController);
            var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            
            #endregion
           
            
            // Scene
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(countOfDetails,GameObjectFactory,_gameContextWithLogic);
            var GameObjectInitilization = new GameObjectInitialization(Pool);

            _controllers = new Controllers();
           // _controllers.Add(GameContextWithViewCreator);
            _controllers.Add(DataBaseController);
            _controllers.Add(abstractView);
            _controllers.Add(abstractFactoryController);
            //_controllers.Add(uiController);
            // _controllers.Add(AuthController);
            _controllers.Add(_fileManagerController);
            _controllers.Initialization();
        }
        
        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.LateExecute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.CleanData();
        }
    }
}