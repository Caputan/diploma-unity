using System;
using Diploma.Controllers;
using Diploma.Controllers.AssembleController;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using GameContextWithUI = Diploma.PracticeScene.GameContext.GameContextWithUI;

namespace Controllers.PracticeScene.UIController
{
    public class UIController: IInitialization, IExecute, ICleanData
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly PauseController.PauseController _pauseController;
        private readonly PlayerInitialization _playerInitialization;
        private readonly KeyCode _pauseKeyCode = KeyCode.Escape;
        public bool _pauseParam;

        public UIController(
            GameContextWithUI gameContextWithUI,
            PauseController.PauseController pauseController,
            PlayerInitialization playerInitialization
            )
        {
            _gameContextWithUI = gameContextWithUI;
           
            _pauseController = pauseController;
            _playerInitialization = playerInitialization;
            _pauseParam = false;
            SetCursorParameters(false);
            SetPlayersRotationAndMovement(false);
        }

        public void Initialization()
        {
            foreach (var value in _gameContextWithUI.UILogic.Values)
            {
                if (value is IPauseButtons)
                {
                    var i = (IPauseButtons) value;
                    i.LoadNext += ShowUIByUIType;
                }
            }
            
            HideUI(_gameContextWithUI.UiControllers[PauseButtons.PauseMenu]);
        }

        private void ShowUIByUIType(PauseButtons obj)
        {
            switch (obj)
            {
                case PauseButtons.Resume:
                    ActivatePauseMenu(_pauseParam);
                    SetCursorParameters(_pauseParam);
                    SetPlayersRotationAndMovement(_pauseParam);
                    _pauseParam = !_pauseParam;
                    break;
                case PauseButtons.Restart:
                    _pauseController.Restart();
                    break;
                case PauseButtons.BackToMenu:
                    _pauseController.BackToMenu();
                    break;
            }
        }

        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown(_pauseKeyCode))
            {
                ActivatePauseMenu(_pauseParam);
                SetCursorParameters(_pauseParam);
                SetPlayersRotationAndMovement(_pauseParam);
                _pauseParam = !_pauseParam;
            }
        }

        public void SetPlayersRotationAndMovement(bool pause)
        {
            _playerInitialization.SetPause(pause);
        }
        
        public void SetCursorParameters(bool isOnOrOff)
        {
            if (isOnOrOff)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void ActivatePauseMenu(bool activateOrDeactivate)
        {
            if (activateOrDeactivate)
            {
                ShowPauseMenu();
                Time.timeScale = 0;
            }
            else
            {
                HideUI(_gameContextWithUI.UiControllers[PauseButtons.PauseMenu]);
                Time.timeScale = 1;
            }
        }
        private void HideUI(GameObject Controller)
        {
            Controller.SetActive(false);
        }
        private void ShowPauseMenu()
        {
            _gameContextWithUI.UiControllers[PauseButtons.PauseMenu].SetActive(true);
        }

        public void CleanData()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                if (value.Value is IPauseButtons)
                {
                    var i = (IPauseButtons) value.Value;
                    i.LoadNext -= ShowUIByUIType;
                }
            }
        }
    }
}