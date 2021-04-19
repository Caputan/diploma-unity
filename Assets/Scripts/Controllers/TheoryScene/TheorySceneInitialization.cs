using System;
using System.Collections.Generic;
using System.IO;
using Controllers.TheoryScene.TheoryControllers;
using Controllers.TheoryScene.UIController;
using Data;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using UnityEngine;
using Types = Diploma.Tables.Types;

namespace Controllers.TheoryScene
{
    public sealed class TheorySceneInitialization: MonoBehaviour
    {
        [SerializeField] private ImportantDontDestroyData _data;
        [SerializeField] private AdditionalInfomationLibrary _library;
        
        [SerializeField] private GameObject canvas;

        [SerializeField] private GameObject theoryPrefab;
        [SerializeField] private GameObject pdfPrefab;
        [SerializeField] private GameObject libraryPrefab;

        [SerializeField] private string _pngStoragePath = "LocalPDFDocumentsInImages";
       
        private Transform _theoryParent;
        private Transform _treeParent;

        private GameContextWithViewsTheory _gameContextWithViewsTheory;
        private GameContextWithUITheory _gameContextWithUITheory;
        private Diploma.Controllers.Controllers _controllers;
        private FileManager _fileManager;
        private void Start()
        {
            
            _gameContextWithViewsTheory = new GameContextWithViewsTheory();
            _gameContextWithUITheory = new GameContextWithUITheory();
            
            //не работает тк обращается к префабу
            _theoryParent = theoryPrefab.transform;
            _treeParent = theoryPrefab.transform;
            //
            _fileManager = new FileManager();
            _pngStoragePath =Path.Combine(_fileManager.GetStorage(),
                _pngStoragePath);
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
            tables.Add(videos);
            
            DataBaseController.SetTable(tables[1]);
            Lessons lesson = (Lessons)DataBaseController.GetRecordFromTableById(_data.lessonID);
            DataBaseController.SetTable(tables[2]);
            Texts pdf = (Texts)DataBaseController.GetRecordFromTableById(lesson.Lesson_Text_Id);
            DataBaseController.SetTable(tables[3]);
            Types Types = (Types) DataBaseController.GetRecordFromTableById(lesson.Lesson_Type_Id);

            LoadingSceneController loadingSceneController = new LoadingSceneController();
            
            TheoryUIInitialization theoryUIInitialization = new TheoryUIInitialization
            (
                canvas,
                theoryPrefab,
                _gameContextWithViewsTheory,
                _gameContextWithUITheory
                );

            PdfReaderUIInitialization pdfReaderUIInitialization = new PdfReaderUIInitialization
            (
                pdfPrefab,
                _fileManager,
                _gameContextWithViewsTheory
            );
            
            TheoryController theoryController = new TheoryController
                (
                pdfReaderUIInitialization,
                pdf.Text_Link,
                _fileManager,
                _pngStoragePath,
                _gameContextWithViewsTheory,
                theoryUIInitialization
                );
           
            LibraryTreeUIInitialization libraryTreeUIInitialization = new LibraryTreeUIInitialization
                (
                libraryPrefab,
                _gameContextWithViewsTheory,
                Types.TypeS,
                _library
            );

            // TheoryController libraryController = new TheoryController(
            //     pdfReaderUIInitialization,
            //     /*library,*/,
            //     _fileManager,
            //     _pngStoragePath,//нужен другой временный буфер
            //     _gameContextWithViewsTheory,
            //     theoryUIInitialization
            //     );

           
            
            _controllers = new Diploma.Controllers.Controllers();
            _controllers.Add(theoryUIInitialization);
            _controllers.Add(pdfReaderUIInitialization);
            //_controllers.Add(theoryController);
            _controllers.Add(libraryTreeUIInitialization);
            //_controllers.Add(libraryTreeController);
            
            _controllers.Initialization();
            
            UIControllerTheoryScene uiControllerTheoryScene = new UIControllerTheoryScene(
                _gameContextWithViewsTheory,
                _gameContextWithUITheory,
                loadingSceneController,
                theoryController,
                theoryController
            );
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void OnDestroy()
        {
            _controllers.CleanData();
        }

        
    }
}