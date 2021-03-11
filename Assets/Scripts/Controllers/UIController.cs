using System;
using System.Collections.Generic;
using System.Diagnostics;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Diploma.Controllers
{
    public class UIController : IInitialization
    {
        private readonly Dictionary<LoadingParts, IUIObjectsFactory> _uiControllers;
        private readonly Transform _parent;

        public UIController(Transform parent)
        {
            _uiControllers = new Dictionary<LoadingParts, IUIObjectsFactory>();
            _parent = parent;
        }

        private void CreateUI(IUIObjectsFactory uiController)
        {
            var go = uiController.Create(_parent);
        }

        private void HideUI(IUIObjectsFactory uiController)
        {
            
        }

        public void AddUIToDictionary(LoadingParts loadingPart, IUIObjectsFactory uiController)
        {
            if (!_uiControllers.ContainsKey(loadingPart))
            {
                _uiControllers.Add(loadingPart, uiController);
            }
        }

        public void RemoveUIFromDictionary(LoadingParts loadingPart, IUIObjectsFactory uiController)
        {
            if (_uiControllers.ContainsKey(loadingPart))
            {
                _uiControllers.Remove(loadingPart);
            }
        }

        public void ShowUIByUIType(LoadingParts id)
        {
            if (_uiControllers.TryGetValue(id, out var uiWindow))
            {
                CreateUI(uiWindow);
            }
        }

        public void HideUIByUIType(LoadingParts id)
        {
            if (_uiControllers.TryGetValue(id, out var uiWindow))
            {
                HideUI(uiWindow);
            }
        }

        public void HideAllUI()
        {
            foreach (var controller in _uiControllers)
            {
                HideUI(controller.Value);
            }
        }

        public bool IsInDictionary(LoadingParts loadingPart)
        {
            return _uiControllers.ContainsKey(loadingPart);
        }

        public void Initialization()
        {
            
        }
    }
}
