using System;
using System.Collections.Generic;
using System.IO;
using Controllers.TheoryScene.TheoryControllers;
using Data;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Tables;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers.TheoryScene
{
    public sealed class TheorySceneInitialization: MonoBehaviour
    {
        [SerializeField] private ImportantDontDestroyData _data;
        
        [SerializeField] private GameObject canvas;

        [SerializeField] private GameObject theoryPrefab;
        [SerializeField] private GameObject pdfPrefab;
        [SerializeField] private GameObject treePrefab;
        [SerializeField] private GameObject libraryPrefab;

        private Transform _theoryParent;
        private Transform _treeParent;
       
        private GameContextWithViewsTheory _gameContextWithViewsTheory;

        private void Start()
        {
            _gameContextWithViewsTheory = new GameContextWithViewsTheory();

            _theoryParent = theoryPrefab.transform;
            _treeParent = treePrefab.transform;
            
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
            Texts pdf = (Texts)DataBaseController.GetRecordFromTableById(lesson.Lesson_Assembly_Id);

            TheoryUIInitialization theoryUIInitialization = new TheoryUIInitialization
            (
                canvas,
                theoryPrefab,
                _gameContextWithViewsTheory
                );

            PdfReaderUIInitialization pdfReaderUIInitialization = new PdfReaderUIInitialization
            (
                pdfPrefab,
                _theoryParent
            );

            TheoryController theoryController = new TheoryController(pdfReaderUIInitialization,pdf.Text_Link);
        }
    }
}