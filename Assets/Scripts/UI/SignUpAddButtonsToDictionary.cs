using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class SignUpAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly int[] _usedMenus = {4, 10, 8, 11};
        
        public SignUpAddButtonsToDictionary(List<Button> buttons, GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddButtons((LoadingParts)_usedMenus[i], button);
                i++;
            }
        }
    }
}