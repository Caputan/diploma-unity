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
            
            var parentForLessons = GameObject.Find("ScrollLessonList/ListViewport/ListContent");
            
            ConstructorButtons = new List<Button>();
            ConstructorButtons.AddRange(Constructor.GetComponentsInChildren<Button>());
            
            new LessonConstructorUIAddButtonsToDictionary(
                ConstructorButtons,
                _gameContextWithViews,
                _gameContextWithLogic,
                _dataBaseController,
                _tables,
                _prefabPlate,
                parentForLessons
                );
            
            var ConstructorLogic = new LessonConstructorUILogic(_gameContextWithViews.LessonConstructorButtons);
            ConstructorLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadCreationOfLesson, Constructor);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadCreationOfLesson,ConstructorLogic);
            
            #endregion
        }
    }
}