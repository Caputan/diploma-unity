﻿using System.Collections.Generic;
using Controllers;
using Diploma.Constructor;
using Diploma.Interfaces;
using Diploma.Tables;
using Diploma.UI;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class MainSceneInitialization: MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private GameObject MainMenuPrefab;
        [SerializeField] private GameObject MainParent;

        [SerializeField] private List<Button> MainMenuButtons;
        
        #region Don't Use
        [SerializeField] private GameObject togglePanelPrefab;
        [SerializeField] private GameObject ToggleGroup;

        [SerializeField] private GameObject toggleLessonPrefab;
        [SerializeField] private GameObject ParentForLessons;
        #endregion
        
        [SerializeField] private FileManagerController _fileManager;
        [SerializeField] private Loader3DS _loader3Ds;
        
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private GameContextWithLessons _gameContextWithLessons;
        private GameContextWithUI _gameContextWithUI;
        
        private Controllers _controllers;
        public string[] destinationPath = new string[4];
        private void Start()
        {
            

            #region DataBase initialization
            // потом надо на отдельные инициализаторы разбить чтоль....
            FileManager.FileManager fileManager = new FileManager.FileManager();
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

            var AuthController = new AuthController(DataBaseController, tables);

            #endregion
            
            #region Creation UI and GameContext
            
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithViews = new GameContextWithViews();
            _gameContextWithLessons = new GameContextWithLessons();
            _gameContextWithUI = new GameContextWithUI();
            
            // тут мы создали базове типизированное меню
            var GameContextWithViewCreator = new GameContexWithViewCreator(
                _gameContextWithViews,
                _gameContextWithLogic,
                _gameContextWithLessons,
                _gameContextWithUI,
                ToggleGroup,
                togglePanelPrefab,
                ParentForLessons,
                toggleLessonPrefab,
                DataBaseController,
                tables
                );

            new MainMenuAddButtonsToDictionary(MainMenuButtons,_gameContextWithViews);
            var MainMenuInitilization = new MainMenuInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                MainMenuPrefab
                );
            
            var uiController = new UIController(_gameContextWithUI);
            //uiController.AddUIToDictionary();
            
            // добавить соответствующие менюшки ниже
            // с помощью uiController.AddUIToDictionary()
            #endregion

            _fileManager.DataBaseController = DataBaseController;
            _fileManager.Tables = tables;
            _fileManager.destinationPath = destinationPath;
            
            #region Creation new Lession Module
            // данный регион будет вызываться во время создания урока
            var abstractFactory = new AbstractFactory();
            var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic,_button,_fileManager);
            var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            
            #endregion
           
            

            _controllers = new Controllers();
            _controllers.Add(GameContextWithViewCreator);
            _controllers.Add(DataBaseController);
            _controllers.Add(abstractView);
            _controllers.Add(abstractFactoryController);
            _controllers.Add(uiController);
            _controllers.Add(AuthController);
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