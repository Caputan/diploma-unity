using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LessonsChooseInitialization : IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameContextWithLessons _gameContextWithLessons;
        private readonly GameObject _lessonChooseParent;
        private readonly GameObject _prefabLessonChoose;
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private LessonChooseFactory _lessonChooseFactory;
        private List<Button> _lessonChooseButtons;
        
        public LessonsChooseInitialization(
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            GameContextWithLessons gameContextWithLessons,
            GameObject LessonChooseParent, 
            GameObject PrefabLessonChoose,
            DataBaseController dataBaseController,
            List<IDataBase> tables
        )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLessons = gameContextWithLessons;
            _lessonChooseParent = LessonChooseParent;
            _prefabLessonChoose = PrefabLessonChoose;
            _dataBaseController = dataBaseController;
            _tables = tables;

            _lessonChooseFactory = new LessonChooseFactory(_prefabLessonChoose);
            
        }
        public void Initialization()
        {
            #region Main Menu Creation

            var LessonsChoose = _lessonChooseFactory.Create(_lessonChooseParent.transform);
            LessonsChoose.transform.localPosition = new Vector3(0,0,0);
            
            _lessonChooseButtons = new List<Button>();
            _lessonChooseButtons.AddRange(LessonsChoose.GetComponentsInChildren<Button>());
            
            // List<Button> buttons,GameContextWithViews gameContextWithViews,
            // GameContextWithLessons gameContextWithLessons
            //     ,DataBaseController dataBaseController, IDataBase[] tables,
            //     PlateWithButtonForLessonsFactory plateWithButtonForLessonsFactory,GameObject scrollParentForLessonsView
            var plateWithButtonForLessonsFactory = new PlateWithButtonForLessonsFactory(_prefabLessonChoose);
            
            new LessonChooseAddButtonsToDictionary(
                _lessonChooseButtons,
                _gameContextWithViews,
                _gameContextWithLessons,
                _dataBaseController,
                _tables,
                plateWithButtonForLessonsFactory,
                _lessonChooseParent
                );
            
            var LessonChooseLogic = new LessonChooseLogic(_gameContextWithViews.Buttons);
            LessonChooseLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadLectures, LessonsChoose);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadLectures,LessonChooseLogic);
            
            #endregion
        }
    }
}