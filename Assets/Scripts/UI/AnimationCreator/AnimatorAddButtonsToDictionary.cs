using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using UnityEngine.UI;

namespace UI.AnimationCreator
{
    public sealed class AnimatorAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly int[] _usedMenus = {0,1,2,3,4,5,6};

        public AnimatorAddButtonsToDictionary(List<Button> buttons, GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddAnimatorButtons((AnimatorButtons) _usedMenus[i], button);
                i++;
            }
        }
    }
}