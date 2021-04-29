using System.Collections.Generic;
using Controllers;
using Controllers.MainScene.LessonsControllers;
using Data;
using Diploma.Controllers.AboutControllers;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using TMPro;
using UI.LoadingUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Diploma.Controllers
{
    public class MainSceneInitialization: MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject MainParent;
        
        [SerializeField] private AudioMixer _mainAudioMixer;
        
        [SerializeField] private ImportantDontDestroyData _data;
        [SerializeField] private AdditionalInfomationLibrary _library;
        

        private FileManagerController _fileManager;
        private Loader3DS _loader3Ds;
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private GameContextWithLessons _gameContextWithLessons;
        private GameContextWithUI _gameContextWithUI;
      
        private Controllers _controllers;
       
        public void Start()
        {

            #region DataBase initialization
            
            FileManager fileManager = new FileManager();
            // destinationPath[0] = fileManager.CreateFileFolder("Assemblies");
            // destinationPath[1] = fileManager.CreateFileFolder("Videos");
            // destinationPath[2] = fileManager.CreateFileFolder("Photos");
            // destinationPath[3] = fileManager.CreateFileFolder("Texts");
            
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

            _gameContextWithLogic.MainCamera = _camera;
            
            _fileManager = new FileManagerController();

            var MainMenuInitilization = new MainMenuInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                AuthController
            );

            var AuthInitialization = new AuthInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                AuthController
            );

            var SignUpInitialization = new SignUpInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                AuthController
            );

            // var ErrorMenuInitialization = new ErrorMenuInitialization(
            //     _gameContextWithViews,
            //     _gameContextWithUI,
            //     MainParent,
            //     ErrorMenuPrefab
            // );
            
            var ChooseLessonInitialization = new LessonsChooseInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                _gameContextWithLessons,
                MainParent,
                DataBaseController,
                tables
                );
            
           
            
            var LessonConstructorInitialization = new LessonConstructorUIInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                _gameContextWithLogic,
                MainParent,
                _library
            );
            
            var LessonConstructorController = new LessonConstructorController(
                DataBaseController,
                tables,
                _gameContextWithViews,
                _gameContextWithLessons,
                _gameContextWithUI,
                _gameContextWithLogic,
                _fileManager,
                fileManager
            );
            
            var ScreenShootController = new ScreenShotController();
            
            var BackController = new BackController();
            
            var ExitController = new ExitController();

            var ErrorHandlerInitialization = new ErrorMenuInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent
            );
            
            var OptionsInitialization = new OptionsInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent
            );
            
            var AboutInitialization = new AboutInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent
            );
            
            var OptionsController = new OptionsController(
                _gameContextWithViews,
                _gameContextWithUI,
                _mainAudioMixer
                );
            
            
            
            
            
            var SceneLoader = new LoadingSceneController();

            var loading = new LoadingUILogic(MainParent.transform);
            
            
            var uiController = new UIController(
                _gameContextWithUI,
                ExitController,
                BackController,
                AuthController,
                _fileManager,
                LessonConstructorController,
                OptionsController,
                ScreenShootController,
                loading
            );
            
            var ChooseLessonController = new LessonsChooseController(
                _gameContextWithViews,
                _gameContextWithUI,
                SceneLoader,
                _data,
                uiController,
                loading
            );

            #endregion

            _controllers = new Controllers();
            _controllers.Add(DataBaseController);
            _controllers.Add(AuthController);
            _controllers.Add(MainMenuInitilization);
            _controllers.Add(AuthInitialization);
            _controllers.Add(SignUpInitialization);
            _controllers.Add(BackController);
            _controllers.Add(ChooseLessonInitialization);
            _controllers.Add(LessonConstructorInitialization);
            _controllers.Add(LessonConstructorController);
            _controllers.Add(OptionsInitialization);
            _controllers.Add(OptionsController);
            _controllers.Add(ErrorHandlerInitialization);
            _controllers.Add(ChooseLessonController);
            _controllers.Add(SceneLoader);
            _controllers.Add(loading);
            _controllers.Add(AboutInitialization);
            
            _controllers.Initialization();
            //этот контроллер идет самым последним
            uiController.Initialization();
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