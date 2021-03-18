using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Diploma.UI
{
    public class SignUpLogic: IUIObject, IInitialization
    {
        private readonly Dictionary<LoadingParts, Button> _buttons;
        
        public event Action<LoadingParts> LoadNext;
        
        public SignUpLogic(Dictionary<LoadingParts, Button> buttons)
        {
            _buttons = buttons;
        }

        private void SwitchToNextMenu(LoadingParts loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }

        public void Initialization()
        {
            foreach (var button in _buttons)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(()=> SwitchToNextMenu(button.Key));
            }
        }
    }
}