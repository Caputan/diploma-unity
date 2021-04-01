using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class LessonConstructorUIInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly GameObject _canvas;

        private readonly GameObject _prefabConstructor;
        private readonly GameObject _prefabPlate;
        private LessonConstructorUIFactory _lessonConstructor;
        private List<Button> ConstructorButtons;
        private GameObject parentForLessons;
        
        public LessonConstructorUIInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            GameContextWithLogic gameContextWithLogic,
            DataBaseController dataBaseController,
            List<IDataBase> tables,
            GameObject Canvas,
            GameObject PrefabConstructor,
            GameObject PrefabPlate
            )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLogic = gameContextWithLogic;
            _dataBaseController = dataBaseController;
            _tables = tables;
            _canvas = Canvas;
            _prefabConstructor = PrefabConstructor;
            _prefabPlate = PrefabPlate;
            _lessonConstructor = new LessonConstructorUIFactory(_prefabConstructor);
            
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
                _dataBaseController,
                _tables,
                _prefabPlate,
                parentForLessons
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