using System.Collections.Generic;
using Diploma.Enums;
using Diploma.PracticeScene.GameContext;
using UnityEngine.UI;


namespace UI.PauseUI
{
    public sealed class PauseButtonsAdd
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithView _gameContextWithViews;
        private readonly int[] _usedMenus = {0,1,2};
        
        public PauseButtonsAdd(List<Button> buttons, GameContextWithView gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddPauseButtons((PauseButtons)_usedMenus[i], button);
                i++;
            }
        }
    }
}