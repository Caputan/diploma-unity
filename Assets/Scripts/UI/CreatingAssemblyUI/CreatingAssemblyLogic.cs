using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.CreatingAssemblyUI
{
    public sealed class CreatingAssemblyLogic: ICreatingAssemblyButton, IInitialization
    {
        private readonly Dictionary<AssemblyCreating, Button> _buttons;

        public event Action<AssemblyCreating> LoadNext;

        public CreatingAssemblyLogic(Dictionary<AssemblyCreating, Button> buttons)
        {
            _buttons = buttons;
        }        
        
        public void SwitchToNextMenu(AssemblyCreating loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }
        
        public void Initialization()
        {
            foreach (var button in _buttons)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(() => SwitchToNextMenu(button.Key));
            }
        }
    }
}