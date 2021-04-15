using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Interfaces;
using UI.TheoryUI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class TheoryUIInitialization: IInitialization
    {
        private readonly GameObject _canvas;
        private readonly GameObject _prefabMainWindow;
        private readonly GameContextWithViewsTheory _gameContextWithViews;
        private TheoryUIFactory _theoryUIFactory;
        private List<Button> TheoryUIButtons;
        
        
        public TheoryUIInitialization(
            GameObject canvas,
            GameObject prefabMainWindow,
            GameContextWithViewsTheory gameContextWithViews
            
            )
        {
            _canvas = canvas;
            _prefabMainWindow = prefabMainWindow;
            _gameContextWithViews = gameContextWithViews;

            _theoryUIFactory = new TheoryUIFactory(_prefabMainWindow);
        }
        
        public void Initialization()
        {
            #region Theory UI Creation

            var TheoryUI = _theoryUIFactory.Create(_canvas.transform);
            TheoryUI.transform.localPosition = new Vector3(0,0,0);
            
            
            TheoryUIButtons = new List<Button>();
            TheoryUIButtons.AddRange(TheoryUI.GetComponentsInChildren<Button>());
            
            var LessonConstructorUIAddButtonsToDictionary = new TheoryUIAddButtonsToDictionary(
                TheoryUIButtons,
                _gameContextWithViews
            );
            LessonConstructorUIAddButtonsToDictionary.Initialization();
            var TheoryUILogic = new TheoryUILogic(_gameContextWithViews.TheoryButtons);
            TheoryUILogic.Initialization();
            #endregion
        }
    }
}