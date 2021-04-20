using System;
using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Interfaces;
using UI.TheoryUI;
using UI.TheoryUI.TheoryLibraryTree;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class TheoryUIInitialization: IInitialization, ICleanData
    {
        private readonly GameObject _canvas;
        private readonly GameObject _prefabMainWindow;
        private readonly GameContextWithViewsTheory _gameContextWithViews;
        private readonly GameContextWithUITheory _gameContextWithUITheory;
        private readonly string _types;
        private TheoryUIFactory _theoryUIFactory;
        private List<Button> TheoryUIButtons;
        private GameObject TheoryUI;
        private TheoryUIAddButtonsToDictionary LessonConstructorUIAddButtonsToDictionary;
        private TheoryUILogic TheoryUILogic;
        
        public TheoryUIInitialization(
            GameObject canvas,
            GameObject prefabMainWindow,
            GameContextWithViewsTheory gameContextWithViews,
            GameContextWithUITheory gameContextWithUITheory,
            string types
            )
        {
            _canvas = canvas;
            _prefabMainWindow = prefabMainWindow;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUITheory = gameContextWithUITheory;
            _types = types;

            _theoryUIFactory = new TheoryUIFactory(_prefabMainWindow);
        }
        
        public void Initialization()
        {
            #region Theory UI Creation
            TheoryUI = _theoryUIFactory.Create(_canvas.transform);
            TheoryUI.transform.localPosition = new Vector3(0,0,0);
            
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