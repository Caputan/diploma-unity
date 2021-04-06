﻿using System.Collections.Generic;
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
        private readonly GameObject _lessonPrefab;
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly ScriptableObject _data;
        private LessonChooseFactory _lessonCanvasChooseFactory;
        private List<Button> _lessonChooseButtons;
        
        public LessonsChooseInitialization(
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            GameContextWithLessons gameContextWithLessons,
            GameObject LessonChooseParent, 
            GameObject PrefabLessonChoose,
            GameObject LessonPrefab,
            DataBaseController dataBaseController,
            List<IDataBase> tables,
            ScriptableObject data
        )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLessons = gameContextWithLessons;
            _lessonChooseParent = LessonChooseParent;
            _prefabLessonChoose = PrefabLessonChoose;
            _lessonPrefab = LessonPrefab;

            _dataBaseController = dataBaseController;
            _tables = tables;
            _data = data;

            _lessonCanvasChooseFactory = new LessonChooseFactory(_prefabLessonChoose);
        }
        public void Initialization()
        {
            #region Main Menu Creation

            var LessonsChoose = _lessonCanvasChooseFactory.Create(_lessonChooseParent.transform);
            LessonsChoose.transform.localPosition = new Vector3(0,0,0);
            // решение так себе,но пока что так. может и не пока что.
            var parentForLessons = LessonsChoose.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
            
            _lessonChooseButtons = new List<Button>();
            _lessonChooseButtons.AddRange(LessonsChoose.GetComponentsInChildren<Button>());
            
            // List<Button> buttons,GameContextWithViews gameContextWithViews,
            // GameContextWithLessons gameContextWithLessons
            //     ,DataBaseController dataBaseController, IDataBase[] tables,
            //     PlateWithButtonForLessonsFactory plateWithButtonForLessonsFactory,GameObject scrollParentForLessonsView
            var plateWithButtonForLessonsFactory = new PlateWithButtonForLessonsFactory(_lessonPrefab);
            
            var lessonChooseAddButtonsToDictionary = new LessonChooseAddButtonsToDictionary(
                _lessonChooseButtons,
                _gameContextWithViews,
                _gameContextWithLessons,
                _dataBaseController,
                _tables,
                plateWithButtonForLessonsFactory,
                parentForLessons
                );
            lessonChooseAddButtonsToDictionary.Initialization();
            var LessonChooseLogic = new LessonChooseLogic(_gameContextWithViews.ChooseLessonButtons);
            LessonChooseLogic.Initialization();
            var LessonButtons = new LessonChooseButtonsLogic(_gameContextWithViews.ChoosenLessonToggles);
            LessonButtons.Initialization();
            
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadLectures, LessonsChoose);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadLectures,LessonChooseLogic);
            _gameContextWithViews.SetLessonChooseButtonsLogic(LessonButtons);

            #endregion
        }
    }
}