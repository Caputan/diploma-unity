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
using TMPro;
using UI.LoadingUI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Types = Diploma.Tables.Types;

namespace Controllers.TheoryScene
{
    public sealed class TheorySceneInitialization: MonoBehaviour
    {
        [SerializeField] private ImportantDontDestroyData _data;
        [SerializeField] private AdditionalInfomationLibrary _library;
        [SerializeField] private GameObject canvas;
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private string _pngStoragePath = "LocalPDFDocumentsInImages";
        

        private GameContextWithViewsTheory _gameContextWithViewsTheory;
        private GameContextWithUITheory _gameContextWithUITheory;
        private Diploma.Controllers.Controllers _controllers;
        private FileManager _fileManager;
        private void Start()
        {
            
            _gameContextWithViewsTheory = new GameContextWithViewsTheory();
            _gameContextWithUITheory = new GameContextWithUITheory();

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
            if (lesson.Lesson_Video_Id != -1)
            {
                DataBaseController.SetTable(tables[5]);
                Videos Video = (Videos) DataBaseController.GetRecordFromTableById(lesson.Lesson_Video_Id);
                _gameContextWithViewsTheory.SetVideo(Video.Video_Link);
            }


            LoadingSceneController loadingSceneController = new LoadingSceneController();
            
            TheoryUIInitialization theoryUIInitialization = new TheoryUIInitialization
            (
                canvas,
                _gameContextWithViewsTheory,
                _gameContextWithUITheory,
                Types.TypeS,
                lesson.Lesson_Name
                );

            PdfReaderUIInitialization pdfReaderUIInitialization = new PdfReaderUIInitialization
            (
                _gameContextWithViewsTheory,
                _videoPlayer
            );

            LoadingUILogic loadingUILogic = new LoadingUILogic(
                canvas.transform
            );
            var ExitController = new ExitController(_data);
            TheoryController theoryController = new TheoryController
                (
                pdfReaderUIInitialization,
                pdf,
                _fileManager,
                _pngStoragePath,
                _gameContextWithViewsTheory,
                theoryUIInitialization,loadingUILogic
                );
           
            LibraryTreeUIInitialization libraryTreeUIInitialization = new LibraryTreeUIInitialization
                (
                _gameContextWithViewsTheory,
                Types.TypeS,
                _library
            );

            LibraryTreeController libraryController = new LibraryTreeController(
                pdfReaderUIInitialization,
                _fileManager,
                _pngStoragePath,
                _gameContextWithViewsTheory,loadingUILogic
                ,_library,
                Types
            );

            MainTheoryController mainTheoryController = new MainTheoryController(
                theoryController,
                libraryController,
                loadingUILogic
                );
            
            _controllers = new Diploma.Controllers.Controllers();
            _controllers.Add(theoryUIInitialization);
            _controllers.Add(pdfReaderUIInitialization);
            //_controllers.Add(theoryController);
            _controllers.Add(libraryTreeUIInitialization);
            _controllers.Add(loadingUILogic);
            _controllers.Add(ExitController);
            _controllers.Initialization();
            //loadingUILogic.SetActiveLoading(true);
            UIControllerTheoryScene uiControllerTheoryScene = new UIControllerTheoryScene(
                _gameContextWithViewsTheory,
                _gameContextWithUITheory,
                loadingSceneController,
                theoryController,
                libraryController,
                _library,
                loadingUILogic,
                mainTheoryController
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