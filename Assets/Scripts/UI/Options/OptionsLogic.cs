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
        private readonly Dictionary<OptionsButtons,Slider> _sliders;

        public event Action<OptionsButtons> LoadNext;
        public event Action<OptionsButtons,float> ChangePersent;

        public OptionsLogic(Dictionary<OptionsButtons,Button> buttons,Dictionary<OptionsButtons,Slider> slider)
        {
            _buttons = buttons;
            _sliders = slider;
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

            foreach (var slider in _sliders)
            {
                slider.Value.onValueChanged.RemoveAllListeners();
                slider.Value.onValueChanged.AddListener(delegate {SwitchPersent (slider.Key,slider.Value.value);});
            }
        }

        public void SwitchPersent(OptionsButtons optionsButtons,float persent)
        {
            ChangePersent?.Invoke(optionsButtons,persent);
        }

       
    }
}