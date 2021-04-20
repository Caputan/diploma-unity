using System;
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
        private readonly int[] _usedMenus = new[] {4 ,2, 1};
        private List<int> _usedLibraryItem;
        public TheoryUIAddButtonsToDictionary(
            List<Button> buttons,
            GameContextWithViewsTheory gameContextWithViews,
            string[] types
            )
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            _usedLibraryItem = new List<int>();
            foreach (var type in types)
            {
                if (type != "")
                {
                    _usedLibraryItem.Add(Convert.ToInt32(type));
                }
            }
            int i = 0;
            foreach (var button in _buttons)
            {
                if (i <= 2)
                {
                    _gameContextWithViews.AddTheoryButtons((LoadingPartsTheoryScene) _usedMenus[i], button);
                }
                else 
                    _gameContextWithViews.AddLibraryButtons(_usedLibraryItem[i-3],button);
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