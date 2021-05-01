using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.Options
{
    public class OptionsLogic:  IInitialization,IUIOptions,IUISilder
    {
        private readonly Dictionary<OptionsButtons, Button> _buttons;
        private readonly Slider _slider;

        public event Action<OptionsButtons> LoadNext;
        public event Action<float> ChangePersent;

        public OptionsLogic(Dictionary<OptionsButtons,Button> buttons,Slider slider)
        {
            _buttons = buttons;
            _slider = slider;
        }
        
        public void SwitchToNextMenu(OptionsButtons loadingParts)
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