using System;
using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Diploma.UI
{
    public class AuthLogic: IMenuButton, IInitialization
    {
        private readonly Dictionary<LoadingParts, Button> _buttons;

        public event Action<LoadingParts> LoadNext;
        
        public AuthLogic(Dictionary<LoadingParts, Button> buttons)
        {
            _buttons = buttons;
        }

        public void SwitchToNextMenu(LoadingParts loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }

        public void Initialization()
        {
            foreach (var button in _buttons)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(() => SwitchToNextMenu(button.Key));
            }
        }
    }
}