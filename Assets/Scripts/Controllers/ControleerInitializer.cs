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
        [SerializeField] private int countOfdetails;
        [SerializeField] private Toggle toggle1;
        [SerializeField] private Toggle toggle2;
        [SerializeField] private Toggle toggle3;
        
        private GameContext _gameContext;
        private Controllers _controllers;
        private void Start()
        {
            _gameContext = new GameContext();
            //тут еще нужно разместить создание меню.
            _gameContext.AddToggles(toggle1);
            _gameContext.AddToggles(toggle2);
            _gameContext.AddToggles(toggle3);
            //-----
            
            
            #endregion

            #region DataBase initialization

            var DataBaseController = new DataBaseController();
            AssemliesTable assemblies = new AssemliesTable();
            LessonsTable lessons = new LessonsTable();
            TextsTable texts = new TextsTable();
            TypesTable types = new TypesTable();
            UsersTable users = new UsersTable();
            VideosTable videos = new VideosTable();
            List<IDataBase> tables = new List<IDataBase>();
            tables.Add(assemblies);
            tables.Add(lessons);
            tables.Add(texts);
            tables.Add(types);
            tables.Add(users);
            tables.Add(videos);
            
            DataBaseController.SetTable(types);
            DataBaseController.AddNewRecordToTable(null, "C:/Users/Артем/Desktop/kart.jpg");
            
            #endregion
            
            #region Creation new Lession Module

            var abstractFactory = new AbstractFactory();
            var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic,_button);
            var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            
            #endregion
           
            
            // Scene
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(countOfdetails,GameObjectFactory,_gameContext);
            var GameObjectInitilization = new GameObjectInitialization(Pool);
                        
            _controllers = new Controllers();
            _controllers.Add(new ToggleView(_gameContext));
            
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