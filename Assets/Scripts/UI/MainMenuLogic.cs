using System;
using System.Collections.Generic;
using Diploma.Enums;
using Interfaces;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class MainMenuLogic: IUIMainMenu
    {
        private readonly Dictionary<LoadingParts,Button> _buttons;

        public MainMenuLogic(Dictionary<LoadingParts,Button> buttons)
        {
            _buttons = buttons;
            foreach (var button in _buttons)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(() => SwitchToNextMenu(button.Key));
            }
            
        }

        public void SwitchToNextMenu(LoadingParts loadingParts)
        {
            LoadNext.Invoke(loadingParts);
        }

        public event Action<LoadingParts> LoadNext;
    }
}