using Data;
using Diploma.PracticeScene.Controllers;
using UnityEngine.SceneManagement;

namespace Controllers.PracticeScene.PauseController
{
    public class PauseController
    {
        private readonly ImportantDontDestroyData _data;
        private readonly LoadingSceneController _loadingSceneController;
        private PracticeSceneInitialization _practiceSceneInitialization;
        public PauseController(ImportantDontDestroyData data, LoadingSceneController loadingSceneController)
        {
            _data = data;
            _loadingSceneController = loadingSceneController;
        }

        public void Restart()
        {
            _practiceSceneInitialization.DecompileGameScene(true);
        }

        public void SetAnPracticeScene(PracticeSceneInitialization practiceSceneInitialization)
        {
            _practiceSceneInitialization = practiceSceneInitialization;
        }
        
        public void BackToMenu()
        {
            _loadingSceneController.SetActiveSceneAndLoadIt(0);
        }
    }
}