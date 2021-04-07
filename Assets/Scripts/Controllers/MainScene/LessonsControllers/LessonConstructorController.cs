using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using Diploma.UI;
using GameObjectCreating;
using Interfaces;
using ListOfLessons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LessonConstructorController: IInitialization,INeedScreenShoot
    {
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLessons _gameContextWithLessons;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly FileManagerController _fileManagerController;
        private readonly string[] _destination;
        private Dictionary<LoadingParts, GameObject> _texts;
        private PlateWithButtonForLessonsFactory _plateWithButtonForLessonsFactory;
        private string[] localText = new string[4];
        public LessonConstructorController(
            DataBaseController dataBaseController,
            List<IDataBase>  tables,
            GameContextWithViews gameContextWithViews,
            GameContextWithLessons gameContextWithLessons,
            GameContextWithUI gameContextWithUI,
            GameContextWithLogic gameContextWithLogic,
            FileManagerController fileManagerController,
            GameObject prefabLessonPlate,
            string[] destination
        )
        {
            _dataBaseController = dataBaseController;
            _tables = tables;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLessons = gameContextWithLessons;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLogic = gameContextWithLogic;
            _fileManagerController = fileManagerController;
            _destination = destination;
            _texts = _gameContextWithViews.TextBoxesOnConstructor;

            _plateWithButtonForLessonsFactory = new PlateWithButtonForLessonsFactory(prefabLessonPlate);
        }

        public void Initialization()
        {
            _fileManagerController.newText += SetTextInTextBox;
        }

        public void SetTextInTextBox(LoadingParts loadingParts, string text)
        {
            // добавить очситку при выходе
            //надо передавать полную строку,а тут сокращать.
            switch (loadingParts)
            {
                case LoadingParts.DownloadModel:
                    localText[0] =_destination[0]+ "\\" + text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        localText[0].Split('\\').Last();
                    break;
                case LoadingParts.DownloadVideo:
                    localText[2] = _destination[1]+ "\\" + text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        localText[2].Split('\\').Last();
                    break;
                case LoadingParts.DownloadPDF:
                    localText[1] = _destination[3]+ "\\" + text;
                    _texts[loadingParts].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = 
                        localText[1].Split('\\').Last();
                    break;
                    // destinationPath[0] = fileManager.CreateFileFolder("Assemblies");
                    // destinationPath[1] = fileManager.CreateFileFolder("Videos");
                    // destinationPath[2] = fileManager.CreateFileFolder("Photos");
                    // destinationPath[3] = fileManager.CreateFileFolder("Texts");
            }
        }

        public void CreateALesson()
        {
             //это из меню
             // 0 - assembles
             // 1 - lessons
             // 2 - text
             // 3 - types
             // 4 - users
             // 5 - videos
             //порядок заполнения в бд таблицы Lessons
             // 0 - preview
             // 1 - text
             // 2 - video
             // 3 - assembly
             // 4 - type
             // 5 - name
            
            string[] lessonPacked = new string[6];
            
            // add new assembly
            if (localText[0] != @"Выберите деталь (*.3ds)")
            {
                _dataBaseController.SetTable(_tables[0]);
                string[] assemblyPacked = new string[1];
                assemblyPacked[0] = localText[0];
                _dataBaseController.AddNewRecordToTable(assemblyPacked);
                lessonPacked[3] = _dataBaseController.GetDataFromTable<Assemblies>().Last().Assembly_Id.ToString();
            }

            // add new text
            if (localText[1] != @"Выберите текстовый фаил(*.pdf)")
            {
                _dataBaseController.SetTable(_tables[2]);
                string[] textPacked = new string[1];
                textPacked[0] = localText[1];
                _dataBaseController.AddNewRecordToTable(textPacked);
                lessonPacked[1] = _dataBaseController.GetDataFromTable<Texts>().Last().Text_Id.ToString();  
                //PDFReader pdfReader = new PDFReader();
                //pdfReader.ProcessRequest(textPacked[0],_destination[2]);
            }

            // add new video
            if (localText[2] != @"Выберите видео-фаил (*.mp4)")
            {
                _dataBaseController.SetTable(_tables[5]);
                string[] videoPacked = new string[1];
                videoPacked[0] = localText[2];
                _dataBaseController.AddNewRecordToTable(videoPacked);
                lessonPacked[2] = _dataBaseController.GetDataFromTable<Videos>().Last().Video_Id.ToString();
            }
            //
            
            // add name

            lessonPacked[5] = _texts[LoadingParts.SetNameToLesson].
                GetComponent<TMP_InputField>().text;

            // add screens
            _dataBaseController.SetTable(_tables[0]);
            Assemblies Assembly = (Assemblies)_dataBaseController.GetRecordFromTableById(Convert.ToInt32(lessonPacked[3]));
            
            var GameObjectFactory = new GameObjectFactory();
            var Pool = new PoolOfObjects(GameObjectFactory,_gameContextWithLogic);
            var GameObjectInitilization = new GameObjectInitialization(Pool, Assembly);
            GameObjectInitilization.Initialization();
            
            TakingScreen(_destination[2]+"\\"+lessonPacked[5]+".png");
            lessonPacked[0] = _destination[2] + "\\" + lessonPacked[5]+".png";
            // string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            // DirectoryInfo d = new DirectoryInfo(filepath);
            // var s = d.GetFiles(lessonPacked[5]);
            //
            
            int count = 0;
            foreach (var key in _gameContextWithViews.ChoosenToggles.Values) { 
                if(key.GetComponentInChildren<Toggle>().isOn) 
                    lessonPacked[4] = count.ToString();
                count++;
            }
            
            
            _dataBaseController.SetTable(_tables[1]);
            _dataBaseController.AddNewRecordToTable(lessonPacked);

            AddNewLessonToListOnUI();
        }

        private void AddNewLessonToListOnUI()
        {
            _dataBaseController.SetTable(_tables[1]);

            Lessons lastLessonInDb = _dataBaseController.GetDataFromTable<Lessons>().Last();
            
            var lessonToggle = _plateWithButtonForLessonsFactory.Create(
                _gameContextWithUI.UiControllers[LoadingParts.LoadLectures].transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform);
            lessonToggle.transform.localPosition = new Vector3(0,0,0);
            //var tex = new Texture2D(5, 5);
            //tex.LoadImage(File.ReadAllBytes(lesson.Lesson_Preview));
            //lessonToggle.GetComponentInChildren<RawImage>().texture = tex;

            var lessonName = lessonToggle.GetComponentInChildren<TextMeshProUGUI>();
            lessonName.text = lastLessonInDb.Lesson_Name;
                
            _gameContextWithLessons.AddLessonsView(lastLessonInDb.Lesson_Id,
                new ListOfLessonsView(lastLessonInDb.Lesson_Id,
                    lessonToggle));
            _gameContextWithViews.AddLessonsToggles(lastLessonInDb.Lesson_Id,lessonToggle);
        }

        public event Action<string> TakeScreanShoot;
        public void TakingScreen(string name)
        {
            TakeScreanShoot.Invoke(name);
        }
    }
}