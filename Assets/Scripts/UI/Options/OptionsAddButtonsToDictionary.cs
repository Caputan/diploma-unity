using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using UnityEngine.UI;

namespace UI.Options
{
    public class OptionsAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly List<Slider> _sliders;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly int[] _usedMenus = new[] {5,0,1,2};
        private readonly int[] _sliderParams = new[] {3, 4};

        public OptionsAddButtonsToDictionary(
            List<Button> buttons, 
            List<Slider> sliders,
            GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _sliders = sliders;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddButtonInOptionsDictionary((OptionsButtons)_usedMenus[i],button);
                i++;
            }

            i = 0;
            foreach (var slider in _sliders)
            {
                _gameContextWithViews.SetSlider((OptionsButtons)_sliderParams[i],slider);
                i++;
            }
        }
    }
}