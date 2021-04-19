using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.Options
{
    public class OptionsLogic:  IInitialization,IMenuButton,IUISilder
    {
        private readonly Dictionary<LoadingParts, Button> _buttons;
        private readonly Slider _slider;

        public event Action<LoadingParts> LoadNext;
        public event Action<float> ChangePersent;

        public OptionsLogic(Dictionary<LoadingParts,Button> buttons,Slider slider)
        {
            _buttons = buttons;
            _slider = slider;
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
                button.Value.onClick.AddListener(()=> SwitchToNextMenu(button.Key));
            }
            
            _slider.onValueChanged.RemoveAllListeners();
            _slider.onValueChanged.AddListener(delegate {SwitchPersent (_slider.value);});
        }

        public void SwitchPersent(float persent)
        {
            ChangePersent?.Invoke(persent);
        }

       
    }
}