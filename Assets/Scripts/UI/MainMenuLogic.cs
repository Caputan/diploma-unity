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
    public class MainMenuLogic: IUIMainMenu, IInitialization
    {
        private readonly Dictionary<LoadingParts,Button> _buttons;
        public event Action<LoadingParts> LoadNext;
        
        public MainMenuLogic(Dictionary<LoadingParts,Button> buttons)
        {
            _buttons = buttons;
        }

        public void SwitchToNextMenu(LoadingParts loadingParts)
        {
            Debug.Log("Buling");
            LoadNext.Invoke(loadingParts);
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