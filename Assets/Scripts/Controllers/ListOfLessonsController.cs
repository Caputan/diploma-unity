using System;
using System.Collections.Generic;
using System.IO;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Tables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public sealed class ListOfLessonsController: IInitialization,ICleanData
    {
        private readonly GameContextWithLessons _listOfLessonsViews;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _dataBases;

        public ListOfLessonsController(GameContextWithLessons listOfLessonsViews,
        GameContextWithViews gameContextWithViews,
        DataBaseController dataBaseController,
        List<IDataBase> dataBases)
        {
            _listOfLessonsViews = listOfLessonsViews;
            _gameContextWithViews = gameContextWithViews;
            _dataBaseController = dataBaseController;
            _dataBases = dataBases;
        }

        public void Initialization()
        {
            foreach (var lessonsView in _listOfLessonsViews._lessonsViews)
            {
                lessonsView.Value.ChooseAnotherLession += ShowInfoAbout;
            }
        }

        private void ShowInfoAbout(int id,DataBaseController dataBaseController,
            List<IDataBase> tables)
        {
            dataBaseController.SetTable(tables[1]);
            Lessons lesson = (Lessons)dataBaseController.GetRecordFromTableById(id);
            
            foreach (var key in _gameContextWithViews.ChoosenLessonToggles.Keys)
            {
                if (key == id)
                {
                    var tex = new Texture2D(5, 5);
                    tex.LoadImage(File.ReadAllBytes(lesson.Lesson_Preview));
                    _gameContextWithViews.infoPanel.
                        GetComponentInChildren<RawImage>().texture = tex;
                    
                    dataBaseController.SetTable(tables[2]);
                    Texts texts = (Texts)dataBaseController.GetRecordFromTableById(lesson.Lesson_Text_Id);
                    StreamReader streamReader = new StreamReader(texts.Text_Link);
                    // хз на счет этого. надо протестить.
                    _gameContextWithViews.infoPanel.
                        GetComponentInChildren<TextMeshPro>().text = streamReader.ReadToEnd();
                }
            }
        }

        public void CleanData()
        {
            foreach (var lessonsView in _listOfLessonsViews._lessonsViews)
            {
                lessonsView.Value.ChooseAnotherLession -= ShowInfoAbout;
            }
        }
    }
}