using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class ErrorMenuAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly int[] _usedMenus = { 10 };

        public ErrorMenuAddButtonsToDictionary(List<Button> buttons, GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddErrorMenuButtons((LoadingParts)_usedMenus[i], button);
                i++;
            }
        }
    }
}