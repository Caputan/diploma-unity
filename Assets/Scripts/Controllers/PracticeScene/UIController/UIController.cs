using System;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.PracticeScene.GameContext;
using Interfaces;
using UnityEngine;

namespace Controllers.PracticeScene.UIController
{
    public class UIController: IInitialization, IExecute
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly KeyCode _pauseKeyCode = KeyCode.Escape;
        private bool _pauseParam;

        public UIController(GameContextWithUI gameContextWithUI)
        {
            _gameContextWithUI = gameContextWithUI;
            _pauseParam = false;
        }

        public void Initialization()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                if (value.Value is IPauseButtons)
                {
                    Debug.Log("PauseButtons "+value.Key);
                    var i = (IPauseButtons) value.Value;
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
                    break;
                case PauseButtons.Restart:
                    break;
                case PauseButtons.Back:
                    break;
            }
        }

        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown(_pauseKeyCode))
            {
                ActivatePauseMenu(_pauseParam);
                _pauseParam = !_pauseParam;
            }
            
        }

        private void ActivatePauseMenu(bool activateOrDeactivate)
        {
            if (activateOrDeactivate)
            {
                Time.timeScale = 0;
                ShowPauseMenu();
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
    }
}