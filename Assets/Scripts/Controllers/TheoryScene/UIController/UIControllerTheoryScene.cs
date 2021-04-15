using Controllers.TheoryScene.TheoryControllers;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;

namespace Controllers.TheoryScene.UIController
{
    public class UIControllerTheoryScene: IInitialization, ICleanData
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly ExitController _exitController;
        private readonly BackController _backController;
        private readonly OptionsController _optionsController;
        private readonly TheoryController _theoryController;
        private readonly LoadingSceneController _loadingSceneController;
        private readonly GameObject _backGround;
        private ErrorHandler _errorHandler;
        private LoadingPartsTheoryScene _currentPosition;
        private ErrorCodes _error;

        public UIControllerTheoryScene(GameContextWithUI gameContextWithUI,
            ExitController exitController,
            BackController backController,
            TheoryController theoryController,
            OptionsController optionsController,
            LoadingSceneController loadingSceneController
        )
        {
            _error = ErrorCodes.None;
            _gameContextWithUI = gameContextWithUI;
            _exitController = exitController;
            _backController = backController;
            _theoryController = theoryController;
            _optionsController = optionsController;
            _loadingSceneController = loadingSceneController;
            _backGround = GameObject.Find("BackGround");
            
        }
        public void Initialization()
        {
            throw new System.NotImplementedException();
        }

        public void CleanData()
        {
            throw new System.NotImplementedException();
        }
    }
}