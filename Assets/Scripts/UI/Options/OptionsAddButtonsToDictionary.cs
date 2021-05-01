using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using UnityEngine.UI;

namespace UI.Options
{
    public class OptionsAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly int[] _usedMenus = new[] {4,0,1,2};

        public OptionsAddButtonsToDictionary(List<Button> buttons, GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddButtonInOptionsDictionary((OptionsButtons)_usedMenus[i],button);
                i++;
            }
        }
    }
}