using System.Collections;
using System.Collections.Generic;
using Controllers;
using Diploma.Controllers;
using Diploma.Controllers.AssembleController;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class Test_CreatingLesson
    {
        
        [Test]
        public void Test_CreatingLessonOKPass()
        {
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
            AssemblyCreator assemblyCreator = new AssemblyCreator();
            var _gameContextWithViews = new GameContextWithViews();
            LessonConstructorController lessonConstructorController 
                = new LessonConstructorController(DataBaseController,tables,
                    _gameContextWithViews,
                    new GameContextWithLessons(),
                    new GameContextWithUI(),
                    new GameContextWithLogic(),
                    new FileManagerController(),
                    new FileManager(),
                    assemblyCreator,
                    new RectTransform()
                    );
            lessonConstructorController.Initialization();
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadModel,new GameObject());
            new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform);
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadVideo,new GameObject());
            new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform);
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadVideo].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadPDF,new GameObject());
            new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform);
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].transform.GetChild(0).gameObject.AddComponent<TextMeshProUGUI>();
            lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadModel,
                $@"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\brake",
                @"D:\Unity-Storage\Diploma_Copy\diploma-unity\AssetBundles\StandaloneWindows\brake");
            lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadVideo,
                "Выберите видео-фаил (*.mp4)","Выберите видео-фаил (*.mp4)");
            lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadPDF,
                @"C:\Users\georg\Downloads\Telegram Desktop\Dop.pdf",
                @"C:\Users\georg\Downloads\Telegram Desktop\Dop.pdf");
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.SetNameToLesson,new GameObject());
            new GameObject().transform.SetParent(_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].transform);
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].transform.GetChild(0).gameObject.AddComponent<TMP_InputField>();
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].transform.GetChild(0).gameObject.
                GetComponent<TMP_InputField>().text = "SomeNewLesson";
            
            //CreateAssemblyDis
            ErrorCodes error;
            lessonConstructorController.OpenAnUIInitialization(out error);
            Assert.AreEqual(ErrorCodes.None, error);
            lessonConstructorController.SavingAssemblyDis("1 2 3 4 5");
            //CreateLesson
            lessonConstructorController.CreateALesson();
            Assert.AreEqual(ErrorCodes.None,lessonConstructorController.CheckForErrors());
        }

        [UnityTest]
        public IEnumerator Test_CreatingLessonWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
