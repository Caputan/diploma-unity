using System.Collections.Generic;
using Data;
using Diploma.Constructor;
using Diploma.Interfaces;
using Diploma.Tables;
using GameObjectCreating;
using UnityEngine;



namespace Diploma.Controllers
{
    public class LessonSceneControllerInitialization: MonoBehaviour
    {

        [SerializeField] private ImportantDontDestroyData _data;
        
        private Controllers _controllers;
        public void Start()
        {
            Debug.Log("Loading is done");
            
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
            
            // #region Creation new Lession Module
            // // данный регион будет вызываться во время создания урока
            // var abstractFactory = new AbstractFactory();
            // var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic,_button,_fileManagerController);
            // var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            //
            // #endregion
            //
            //
            // // Scene
            // var GameObjectFactory = new GameObjectFactory();
            // var Pool = new PoolOfObjects(countOfDetails,GameObjectFactory,_gameContextWithLogic);
            // var GameObjectInitilization = new GameObjectInitialization(Pool);
            
            // Loader3DS loader3Ds = new Loader3DS();
            //loader3Ds.StartParsing();
            
            
            _controllers = new Controllers();
            
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