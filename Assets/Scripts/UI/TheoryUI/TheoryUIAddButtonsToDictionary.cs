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
    public class TheoryUIAddButtonsToDictionary: IInitialization,ICleanData
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViewsTheory _gameContextWithViews;
        // нужно определеить кнопки
        private readonly int[] _usedMenus = new[] {4, 1};
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
                if (i < 2)
                {
                    _gameContextWithViews.AddTheoryButtons((LoadingPartsTheoryScene) _usedMenus[i], button);
                    if (i == 0)
                    {
                        _gameContextWithViews.AddTheoryButtons((LoadingPartsTheoryScene) 2, button);
                    }
                }
                else 
                    _gameContextWithViews.AddLibraryButtons(i-2,button);
                i++;
                
            }
        }
        public void CleanData()
        {
            _gameContextWithViews.LibraryButtons.Clear();
            _gameContextWithViews.TheoryButtons.Clear();
        }
        public void Initialization()
        { }

        
    }
}