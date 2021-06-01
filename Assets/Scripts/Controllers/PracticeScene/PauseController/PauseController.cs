using Data;
using UnityEngine.SceneManagement;

namespace Controllers.PracticeScene.PauseController
{
    public class PauseController
    {
        private readonly ImportantDontDestroyData _data;
        private readonly LoadingSceneController _loadingSceneController;

        public PauseController(ImportantDontDestroyData data, LoadingSceneController loadingSceneController)
        {
            _data = data;
            _loadingSceneController = loadingSceneController;
        }

        public void Restart()
        {
            _loadingSceneController.LoadNextScene(2);
        }

        public void BackToMenu()
        {
            _loadingSceneController.LoadNextScene(0);
        }
    }
}