using System.Collections.Generic;
using System.IO;
using Controllers;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using ListOfLessons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class LessonChooseAddButtonsToDictionary: IInitialization
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLessons _gameContextWithLessons;
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly PlateWithButtonForLessonsFactory _plateWithButtonForLessonsFactory;
        private readonly GameObject _scrollParentForLessonsView;
        private int[] _usedButton = new[] {10};
        public LessonChooseAddButtonsToDictionary(List<Button> buttons,GameContextWithViews gameContextWithViews,
            GameContextWithLessons gameContextWithLessons
        ,DataBaseController dataBaseController, List<IDataBase> tables,
        PlateWithButtonForLessonsFactory plateWithButtonForLessonsFactory,GameObject scrollParentForLessonsView
        )
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLessons = gameContextWithLessons;
            _dataBaseController = dataBaseController;
            _tables = tables;
            _plateWithButtonForLessonsFactory = plateWithButtonForLessonsFactory;
            _scrollParentForLessonsView = scrollParentForLessonsView;
            _buttons = buttons;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddChooseLessonButtons((LoadingParts)_usedButton[i],button);
                i++;
            }
        }

        public void Initialization()
        {
            _dataBaseController.SetTable(_tables[1]);
            List<Lessons> dataLessonsFromTable = _dataBaseController.GetDataFromTable<Lessons>();
            int i = 0;
            foreach (var lesson in dataLessonsFromTable)
            {
                var lessonToggle = _plateWithButtonForLessonsFactory.Create(_scrollParentForLessonsView.transform);
                lessonToggle.transform.localPosition = new Vector3(0,0,0);
                //var tex = new Texture2D(5, 5);
                //tex.LoadImage(File.ReadAllBytes(lesson.Lesson_Preview));
                //lessonToggle.GetComponentInChildren<RawImage>().texture = tex;

                var lessonName = lessonToggle.GetComponentInChildren<TextMeshProUGUI>();
                lessonName.text = lesson.Lesson_Type_Id.ToString();
                
                _gameContextWithLessons.AddLessonsView(lesson.Lesson_Id,
                    new ListOfLessonsView(lesson.Lesson_Id,
                        lessonToggle));
                _gameContextWithViews.AddLessonsToggles(lesson.Lesson_Id,lessonToggle);
                
            }
        }
    }
}