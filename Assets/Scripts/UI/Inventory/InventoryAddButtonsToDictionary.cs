using System.Collections.Generic;
using Diploma.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class InventoryAddButtonsToDictionary
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private GameObject[] _partsOfAssembly;

        public InventoryAddButtonsToDictionary(GameObject[] partsOfAssembly, List<Button> buttons, 
            GameContextWithViews gameContextWithViews)
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            _partsOfAssembly = partsOfAssembly;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddInventoryButtons(partsOfAssembly[i], button);
                i++;
            }
        }
    }
}