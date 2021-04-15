using System.Collections.Generic;
using System.IO;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TheoryUI
{
    public class TheoryUIAddButtonsToDictionary: IInitialization
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViewsTheory _gameContextWithViews;
        // нужно определеить кнопки
        private readonly int[] _usedMenus = new[] {3, 2, 8, 11, 15};
        public TheoryUIAddButtonsToDictionary(
            List<Button> buttons,
            GameContextWithViewsTheory gameContextWithViews
            )
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddTheoryButtonsButtons((LoadingPartsTheoryScene)_usedMenus[i],button);
                i++;
            }
        }

        public void Initialization()
        { }
    }
}