using System.Collections.Generic;
using Data;
using Diploma.Constructor;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using GameObjectCreating;
using UnityEngine;



namespace Diploma.Controllers
{
    public class LessonSceneControllerInitialization: MonoBehaviour
    {

        [SerializeField] private ImportantDontDestroyData _data;
        
        
        
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private GameContextWithLessons _gameContextWithLessons;
        private GameContextWithUI _gameContextWithUI;
        private Controllers _controllers;
        public string[] destinationPath = new string[4];
        
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
            
            _gameContextWithLogic = new GameContextWithLogic();
            _gameContextWithViews = new GameContextWithViews();
            _gameContextWithUI = new GameContextWithUI();
            
            DataBaseController.SetTable(tables[1]);
            Lessons lesson = (Lessons)DataBaseController.GetRecordFromTableById(_data.lessonID);
            DataBaseController.SetTable(tables[0]);
            Assemblies Assembly = (Assemblies)DataBaseController.GetRecordFromTableById(lesson.Lesson_Assembly_Id);
            
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(GameObjectFactory,_gameContextWithLogic);
            var GameObjectInitilization = new GameObjectInitialization(Pool, Assembly);
            
            //объекты в пуле
            //надо проверить как они в логике появляются или нет
            // еще надо убрать дубли
            //еще надо переписать забор из пула по запросу
            
            
            _controllers = new Controllers();

            _controllers.Add(GameObjectInitilization);
            
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