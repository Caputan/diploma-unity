using Data;
using Diploma.Controllers;
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

        public LessonsChooseController(
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            LoadingSceneController loadingSceneController,
            ImportantDontDestroyData importantDontDestroyData
        )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _loadingSceneController = loadingSceneController;
            _importantDontDestroyData = importantDontDestroyData;
        }

        public void Initialization()
        {
            _gameContextWithViews.LessonChooseButtonsLogic.LoadLesson += loadScene;
        }

        private void loadScene(int id)
        {
            _importantDontDestroyData.lessonID = id;
            _loadingSceneController.LoadNextScene(1);
        }
    }
}