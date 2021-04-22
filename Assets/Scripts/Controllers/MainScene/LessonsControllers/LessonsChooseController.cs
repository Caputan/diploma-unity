using Data;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public sealed class LessonsChooseController: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly LoadingSceneController _loadingSceneController;
        private readonly ImportantDontDestroyData _importantDontDestroyData;
        private readonly UIController _uiController;

        public LessonsChooseController(
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            LoadingSceneController loadingSceneController,
            ImportantDontDestroyData importantDontDestroyData,
            UIController uiController
        )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _loadingSceneController = loadingSceneController;
            _importantDontDestroyData = importantDontDestroyData;
            _uiController = uiController;
        }

        public void Initialization()
        {
            _gameContextWithViews.LessonChooseButtonsLogic.LoadLesson += LoadScene;
        }

        private void LoadScene(int id)
        {
            _uiController.ShowUIByUIType(LoadingParts.LoadLessonScene);
            _importantDontDestroyData.lessonID = id;
            _loadingSceneController.LoadNextScene(1);
        }
    }
}