using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.PracticeScene.GameContext;
using Tools;
using UI.PauseUI;
using UnityEngine;
using UnityEngine.UI;
using GameContextWithUI = Diploma.PracticeScene.GameContext.GameContextWithUI;

namespace Controllers.PracticeScene.PauseController
{
    public class PauseInitialization: IInitialization
    {
        private readonly GameContextWithView _gameContextWithView;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _pauseParent;
        private PauseFactory _pauseFactory;
        private List<Button> _pauseButtons;

        private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/PracticeScene/Pause" };

        public PauseInitialization(GameContextWithView gameContextWithView,
            GameContextWithUI gameContextWithUI,
            GameObject pauseParent
        )
        {
            _gameContextWithView = gameContextWithView;
            _gameContextWithUI = gameContextWithUI;
            _pauseParent = pauseParent;
            _pauseFactory = new PauseFactory(ResourceLoader.LoadPrefab(_viewPath));
        }

        public void Initialization()
        {
            var pause = _pauseFactory.Create(_pauseParent.transform);
            pause.transform.localPosition = new Vector3(0,0,0);
            
            _pauseButtons = new List<Button>();
            _pauseButtons.AddRange(pause.GetComponentsInChildren<Button>());

            new PauseButtonsAdd(_pauseButtons, _gameContextWithView);
            
            var pauseLogic = new PauseLogic(_gameContextWithView.PauseButtons);
            pauseLogic.Initialization();

            _gameContextWithUI.AddUIToDictionary(PauseButtons.PauseMenu, pause);
            _gameContextWithUI.AddUILogic(PauseButtons.PauseMenu, pauseLogic);
        }
    }
}