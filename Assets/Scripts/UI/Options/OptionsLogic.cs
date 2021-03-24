using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.Options
{
    public class OptionsLogic: IUIObject, IInitialization
    {
        private readonly Dictionary<LoadingParts, Button> _buttons;

        public OptionsLogic(Dictionary<LoadingParts,Button> buttons)
        {
            _buttons = buttons;
        }
        
        public void SwitchToNextMenu(LoadingParts loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }

        public event Action<LoadingParts> LoadNext;
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