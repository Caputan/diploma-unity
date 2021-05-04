using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PauseUI
{
    public class PauseLogic: IPauseButtons, IInitialization
    {
        private readonly Dictionary<PauseButtons, Button> _buttons;
        
        public event Action<PauseButtons> LoadNext;
        
        public PauseLogic(Dictionary<PauseButtons, Button> buttons)
        {
            _buttons = buttons;
        }

        public void SwitchToNextMenu(PauseButtons loadingParts)
        {
            Debug.Log("Button is pressed");
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