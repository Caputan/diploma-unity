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
    public class UIController : IInitialization
    {
        private readonly GameContextWithUI _gameContextWithUI;

        public UIController(GameContextWithUI gameContextWithUI)
        {
            _gameContextWithUI = gameContextWithUI;
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
            _gameContextWithUI.UiControllers[id].SetActive(true);
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

     
    }
}
