using System;
using System.Collections.Generic;
using System.Diagnostics;
using Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Diploma.Controllers
{
    public class UIController : IInitialization, ICleanData
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly ExitController _exitController;

        public UIController(GameContextWithUI gameContextWithUI,ExitController exitController)
        {
            _gameContextWithUI = gameContextWithUI;
            _exitController = exitController;
        }

        public void Initialization()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                var i = (IUIMainMenu) value.Value;
                i.LoadNext += ShowUIByUIType;
            }
        }

        private void HideUI(GameObject Controller)
        {
            Controller.SetActive(false);
        }

        public void ShowUIByUIType(LoadingParts id)
        {
            HideAllUI();
            switch (id)
            {
                case LoadingParts.Exit:
                    _exitController.ExitApplication(); 
                    break;
                case LoadingParts.LoadAuth:
                    
                    break;
                case LoadingParts.LoadLectures:
                    
                    break;
                case LoadingParts.LoadConstructor:
                    
                    break;
                case LoadingParts.Options:
                    
                    break;
            }
            // нужно обращать к контроллеру
            //_gameContextWithUI.UiControllers[id].SetActive(true);
            Debug.Log(id);
        }

        public void HideUIByUIType(LoadingParts id)
        {
            
        }

        public void HideAllUI()
        {
            foreach (var controller in _gameContextWithUI.UiControllers)
            {
                HideUI(controller.Value);
            }
        }


        public void CleanData()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                var i = (IUIMainMenu) value.Value;
                i.LoadNext += ShowUIByUIType;
            }
        }
    }
}
