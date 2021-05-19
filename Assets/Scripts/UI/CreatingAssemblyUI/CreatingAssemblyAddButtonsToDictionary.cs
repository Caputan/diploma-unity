using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using UnityEngine.UI;

namespace UI.CreatingAssemblyUI
{
    public sealed class CreatingAssemblyAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly int[] _usedMenus = {0,1,2};
        
        public CreatingAssemblyAddButtonsToDictionary(List<Button> buttons, GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddAssemblyCreatingMenuButtons((AssemblyCreating) _usedMenus[i], button);
                i++;
            }
        }
    }
}