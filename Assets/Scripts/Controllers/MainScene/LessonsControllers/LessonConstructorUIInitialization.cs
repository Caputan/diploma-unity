using System.Collections.Generic;
using Data;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LessonConstructorUIInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly GameObject _canvas;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/LessonConstructor"};
        private readonly ResourcePath _viewPathForPlate = new ResourcePath {PathResource = "Prefabs/MainScene/LessonConstructorPanel"};
        
        
        private readonly GameObject _prefabConstructor;
        private readonly AdditionalInfomationLibrary _additionalInfomationLibrary;
        private LessonConstructorUIFactory _lessonConstructor;
        private List<Button> ConstructorButtons;
        private GameObject parentForLessons;
        
        public LessonConstructorUIInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            GameContextWithLogic gameContextWithLogic,
            GameObject Canvas,
            AdditionalInfomationLibrary additionalInfomationLibrary
            )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLogic = gameContextWithLogic;
            _canvas = Canvas;
            
            _additionalInfomationLibrary = additionalInfomationLibrary;
            _lessonConstructor = new LessonConstructorUIFactory(ResourceLoader.LoadPrefab(_viewPath));
            
        }
        public void Initialization()
        {
            #region Lesson constructor Creation

            var Constructor = _lessonConstructor.Create(_canvas.transform);
            Constructor.transform.localPosition = new Vector3(0,0,0);
            
            parentForLessons = Constructor.transform.GetChild(10).GetChild(0).GetChild(0).gameObject;
            
            ConstructorButtons = new List<Button>();
            ConstructorButtons.AddRange(Constructor.GetComponentsInChildren<Button>());
            
            var LessonConstructorUIAddButtonsToDictionary = new LessonConstructorUIAddButtonsToDictionary(
                ConstructorButtons,
                _gameContextWithViews,
                _gameContextWithLogic,
                ResourceLoader.LoadPrefab(_viewPathForPlate),
                parentForLessons,
                _additionalInfomationLibrary
                );
            LessonConstructorUIAddButtonsToDictionary.Initialization();
            var ConstructorLogic = new LessonConstructorUILogic(_gameContextWithViews.LessonConstructorButtons);
            ConstructorLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadCreationOfLesson, Constructor);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadCreationOfLesson,ConstructorLogic);
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadModel,
                Constructor.transform.GetChild(4).gameObject);
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadPDF,
                Constructor.transform.GetChild(6).gameObject);
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.DownloadVideo,
                Constructor.transform.GetChild(8).gameObject);
            _gameContextWithViews.AddTextBoxesToListInConstructor(LoadingParts.SetNameToLesson,
                Constructor.transform.GetChild(11).gameObject);

            #endregion
        }
    }
}