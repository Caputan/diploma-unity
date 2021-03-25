using System.Collections.Generic;
using Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;


namespace Diploma.Controllers
{
    public class MainSceneInitialization: MonoBehaviour
    {
        //[SerializeField] private Button _button;
        [SerializeField] private GameObject MainMenuPrefab;
        [SerializeField] private GameObject MainParent;
        
        [SerializeField] private GameObject AuthPrefab;
        
        [SerializeField] private GameObject SignUpPrefab;

        [SerializeField] private GameObject lessonCanvasPrefab;
        [SerializeField] private GameObject lessonPrefab;
        [SerializeField] private GameObject ParentForLessons;
        
        [SerializeField] private GameObject lessonConstructorPrefab;
        [SerializeField] private GameObject lessonConstructorPlatePrefab;

        [SerializeField] private GameObject OptionsPrefab;

        [SerializeField] private AudioMixer _mainAudioMixer;
        
        [SerializeField] private GameObject ErrorMenuPrefab;
        
        #region Don't Use
        // [SerializeField] private GameObject togglePanelPrefab;
        // [SerializeField] private GameObject ToggleGroup;

        
        #endregion
        
        [SerializeField] private FileManagerController _fileManager;
        [SerializeField] private Loader3DS _loader3Ds;
        
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithViews _gameContextWithViews;
        private GameContextWithLessons _gameContextWithLessons;
        private GameContextWithUI _gameContextWithUI;
      
        private Controllers _controllers;
        public string[] destinationPath = new string[4];
        public void Start()
        {
            

            #region DataBase initialization
            // потом надо на отдельные инициализаторы разбить чтоль....
            FileManager fileManager = new FileManager();
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
            
            
            
            _fileManager = new FileManagerController(_gameContextWithUI,destinationPath);
            // тут мы создали базове типизированное меню
            // var GameContextWithViewCreator = new GameContexWithViewCreator(
            //     _gameContextWithViews,
            //     _gameContextWithLogic,
            //     _gameContextWithLessons,
            //     _gameContextWithUI,
            //     ToggleGroup,
            //     togglePanelPrefab,
            //     ParentForLessons,
            //     toggleLessonPrefab,
            //     DataBaseController,
            //     tables
            //     );


            var MainMenuInitilization = new MainMenuInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                MainMenuPrefab,
                AuthController
            );

            var AuthInitialization = new AuthInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                AuthPrefab,
                AuthController
            );

            var SignUpInitialization = new SignUpInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                SignUpPrefab,
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
                ParentForLessons,
                lessonCanvasPrefab,
                lessonPrefab,
                DataBaseController,
                tables
                );
            
            var LessonConstructorInitialization = new LessonConstructorUIInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                _gameContextWithLogic,
                DataBaseController,
                tables,
                MainParent,
                lessonConstructorPrefab,
                lessonConstructorPlatePrefab
            );
            
            var LessonConstructorController = new LessonConstructorController(
                DataBaseController,
                tables,
                _gameContextWithViews,
                _gameContextWithLessons,
                _gameContextWithUI,
                _fileManager,
                lessonPrefab
            );
            
            var BackController = new BackController();
            
            var ExitController = new ExitController();
            
            var OptionsInitialization = new OptionsInitialization(
                _gameContextWithViews,
                _gameContextWithUI,
                MainParent,
                OptionsPrefab
            );
            
            var OptionsController = new OptionsController(
                _gameContextWithViews,
                _gameContextWithUI,
                _mainAudioMixer
                );
            
            var uiController = new UIController(
                _gameContextWithUI,
                ExitController,
                BackController,
                AuthController,
                _fileManager,
                LessonConstructorController,
                OptionsController
                );
            //uiController.AddUIToDictionary();
            // добавить соответствующие менюшки ниже
            // с помощью uiController.AddUIToDictionary()
            #endregion
            
            // _fileManager.DataBaseController = DataBaseController;
            // _fileManager.Tables = tables;
            // _fileManager.destinationPath = destinationPath;
            
            #region Creation new Lession Module
            // данный регион будет вызываться во время создания урока
            //var abstractFactory = new AbstractFactory();
            //var abstractView = new AbstractView(_gameContextWithViews,_gameContextWithLogic,MainMenuButtons[0],_fileManager);
            //var abstractFactoryController = new AbstractFactoryController(abstractView,abstractFactory);
            
            #endregion
           
            

            _controllers = new Controllers();
            // _controllers.Add(GameContextWithViewCreator);
            _controllers.Add(DataBaseController);
            // _controllers.Add(abstractView);
            // _controllers.Add(abstractFactoryController);
            _controllers.Add(AuthController);
            _controllers.Add(MainMenuInitilization);
            _controllers.Add(AuthInitialization);
            _controllers.Add(SignUpInitialization);
            //_controllers.Add(ErrorMenuInitialization);
            _controllers.Add(BackController);
            _controllers.Add(ChooseLessonInitialization);
            _controllers.Add(LessonConstructorInitialization);
            _controllers.Add(LessonConstructorController);
            _controllers.Add(OptionsInitialization);
            _controllers.Add(OptionsController);
            //этот контроллер идет самым последним
            _controllers.Add(uiController);
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