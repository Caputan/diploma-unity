using System;
using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Interfaces;
using TMPro;
using Tools;
using UI.TheoryUI;
using UI.TheoryUI.TheoryLibraryTree;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class TheoryUIInitialization: IInitialization, ICleanData
    {
        private readonly GameObject _canvas;
        private readonly GameContextWithViewsTheory _gameContextWithViews;
        private readonly GameContextWithUITheory _gameContextWithUITheory;
        private readonly string _types;
        private readonly string _nameOfLesson;
        private TheoryUIFactory _theoryUIFactory;
        private List<Button> TheoryUIButtons;
        private GameObject TheoryUI;
        private TheoryUIAddButtonsToDictionary LessonConstructorUIAddButtonsToDictionary;
        private TheoryUILogic TheoryUILogic;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/TheoryScene/Theory"};
        
        public TheoryUIInitialization(
            GameObject canvas,
            GameContextWithViewsTheory gameContextWithViews,
            GameContextWithUITheory gameContextWithUITheory,
            string types,
            string nameOfLesson
            )
        {
            _canvas = canvas;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUITheory = gameContextWithUITheory;
            _types = types;
            _nameOfLesson = nameOfLesson;

            _theoryUIFactory = new TheoryUIFactory(ResourceLoader.LoadPrefab(_viewPath));
        }
        
        public void Initialization()
        {
            #region Theory UI Creation
            TheoryUI = _theoryUIFactory.Create(_canvas.transform);
            TheoryUI.transform.localPosition = new Vector3(0,0,0);
            TheoryUI.GetComponentInChildren<TextMeshProUGUI>().text = "Урок: "+_nameOfLesson;
            _gameContextWithViews.AddParentsToList(TheoryUI.transform.GetChild(5).GetChild(0).GetChild(0));
            _gameContextWithViews.AddParentsToList(TheoryUI.transform.GetChild(6).GetChild(0).GetChild(0));
            #endregion
        }
        
        public void SetButtons()
        {
            TheoryUIButtons = new List<Button>();
            TheoryUIButtons.AddRange(TheoryUI.GetComponentsInChildren<Button>());
            var usingTypes =_types.Split(',');
            LessonConstructorUIAddButtonsToDictionary  = new TheoryUIAddButtonsToDictionary(
                TheoryUIButtons,
                _gameContextWithViews,
                usingTypes
            );
            LessonConstructorUIAddButtonsToDictionary.Initialization();

            foreach (var button in _gameContextWithViews.TheoryButtons)
            {
                TheoryUILogic = new TheoryUILogic(button.Key,button.Value);
                TheoryUILogic.Initialization();
                _gameContextWithUITheory.AddUILogic(button.Key,TheoryUILogic);
            }

            foreach (var button in _gameContextWithViews.LibraryButtons)
            {
                TheoryLibraryTreeLogic treeLogic = new TheoryLibraryTreeLogic(button.Key,button.Value);
                treeLogic.Initialization();
                _gameContextWithUITheory.AddUITreeLogic(button.Key,treeLogic);
            }
        }

        public void CleanData()
        {
            LessonConstructorUIAddButtonsToDictionary.CleanData();
            TheoryUILogic.CleanData();
        }
    }
}