using System;
using System.Collections.Generic;
using Controllers;
using Diploma.Constructor;
using Diploma.Interfaces;
using Diploma.Tables;
using Diploma.UI;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.UI;
using Types = Diploma.Tables.Types;


namespace Diploma.Controllers
{
    public class ControleerInitializer : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private int countOfDetails;
        [SerializeField] private GameObject togglePanelPrefab;
        [SerializeField] private GameObject ToggleGroup;
        [SerializeField] private FileManagerController _fileManager;
        [SerializeField] private Loader3DS _loader3Ds;
        
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private Controllers _controllers;
        private void Start()
        {
            

            #region DataBase initialization

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
            
            //DataBaseController.SetTable(types);
            //DataBaseController.AddNewRecordToTable(null, "C:/Users/Артем/Desktop/kart.jpg");
            #endregion
            
            #region Creation UI and GameContext
            
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithViews = new GameContextWithViews();
           
            // тут мы создали базове типизированное меню
            var GameContextWithViewCreator = new GameContexWithViewCreator(
                _gameContextWithViews,
                _gameContextWithLogic,
                ToggleGroup,
                togglePanelPrefab,
                DataBaseController,
                tables
                );
            
            #endregion

            _fileManager.DataBaseController = DataBaseController;
            _fileManager.Tables = tables;
            #region Creation new Lession Module
            // данный регион будет вызываться во время создания урока
            var abstractFactory = new AbstractFactory();
            var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic,_button,_fileManager);
            var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            
            #endregion
           
            
            // Scene
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(countOfDetails,GameObjectFactory,_gameContextWithLogic);
            var GameObjectInitilization = new GameObjectInitialization(Pool);

            _controllers = new Controllers();
            _controllers.Add(GameContextWithViewCreator);
            _controllers.Add(DataBaseController);
            _controllers.Add(abstractView);
            _controllers.Add(abstractFactoryController);
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