using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.About
{
    public sealed class AboutLogic: IMenuButton, IInitialization
    {
        public event Action<LoadingParts> LoadNext;
        
        private readonly Dictionary<LoadingParts,Button> _buttonLogic;

        
        public AboutLogic(Dictionary<LoadingParts,Button> buttonLogic
        )
        {
            _buttonLogic = buttonLogic;
            
        }
        public void Initialization()
        {
            foreach (var button in _buttonLogic)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(()=> SwitchToNextMenu(button.Key));
            }
        }

        public void SwitchToNextMenu(LoadingParts loadingParts)
        {
            LoadNext.Invoke(loadingParts);
        }
    }
}