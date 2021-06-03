using System.Collections.Generic;
using Diploma.Enums;
using Diploma.PracticeScene.GameContext;
using UnityEngine.UI;

namespace UI.CompleteUI
{
    public class CompleteButtonsAdd
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithView _gameContextWithViews;
        private readonly int[] _usedMenus = { 0 };
    
        public CompleteButtonsAdd(List<Button> buttons, GameContextWithView gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddCompleteButtons((CompleteButtons)_usedMenus[i], button);
                i++;
            }
        }
    }
}