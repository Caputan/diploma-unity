using System;
using System.Collections.Generic;
using System.Diagnostics;
using Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Diploma.Controllers
{
    public class UIController : IInitialization
    {
        private readonly GameContextWithUI _gameContextWithUI;

        public UIController(GameContextWithUI gameContextWithUI)
        {
            _gameContextWithUI = gameContextWithUI;
        }

        // Данный контрллер должен только уметь прять и показывать
        // На входе будет только GameContextWithUI
        
        // private void CreateUI(IUIObjectsFactory uiController)
        // {
        //     var go = uiController.Create(_parents);
        // }

        private void HideUI(IUIObjectsFactory uiController)
        {
            
        }

        public void ShowUIByUIType(LoadingParts id)
        {
            // if (_uiControllers.TryGetValue(id, out var uiWindow))
            // {
            //     CreateUI(uiWindow);
            // }
        }

        public void HideUIByUIType(LoadingParts id)
        {
            // if (_uiControllers.TryGetValue(id, out var uiWindow))
            // {
            //     HideUI(uiWindow);
            // }
        }

        public void HideAllUI()
        {
            // foreach (var controller in _uiControllers)
            // {
            //     HideUI(controller.Value);
            // }
        }

        public void Initialization() { }
    }
}
