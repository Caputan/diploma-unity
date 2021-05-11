using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.UI;
using Tools;
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
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly FileManager _fileManager;
        private LessonChooseFactory _lessonCanvasChooseFactory;
        private List<Button> _lessonChooseButtons;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/SelectLession"};
        private readonly ResourcePath _viewPathForPlate = new ResourcePath {PathResource = "Prefabs/MainScene/LessonPrefab"};

        
        public LessonsChooseInitialization(
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            GameContextWithLessons gameContextWithLessons,
            GameObject LessonChooseParent,
            DataBaseController dataBaseController,
            List<IDataBase> tables,
            FileManager fileManager
        )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLessons = gameContextWithLessons;
            _lessonChooseParent = LessonChooseParent;
            _dataBaseController = dataBaseController;
            _tables = tables;
            _fileManager = fileManager;

            _lessonCanvasChooseFactory = new LessonChooseFactory(ResourceLoader.LoadPrefab(_viewPath));
        }
        public void Initialization()
        {
            #region Main Menu Creation

            var LessonsChoose = _lessonCanvasChooseFactory.Create(_lessonChooseParent.transform);
            LessonsChoose.transform.localPosition = new Vector3(0,0,0);
            var parentForLessons = LessonsChoose.transform.GetChild(2).GetChild(0).GetChild(0).gameObject;
            
            _lessonChooseButtons = new List<Button>();
            _lessonChooseButtons.AddRange(LessonsChoose.GetComponentsInChildren<Button>());
            
            var plateWithButtonForLessonsFactory = new PlateWithButtonForLessonsFactory(ResourceLoader.LoadPrefab(_viewPathForPlate));
            
            var lessonChooseAddButtonsToDictionary = new LessonChooseAddButtonsToDictionary(
                _lessonChooseButtons,
                _gameContextWithViews,
                _gameContextWithLessons,
                _dataBaseController,
                _tables,
                plateWithButtonForLessonsFactory,
                parentForLessons,
                _fileManager
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