using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.TheoryUI
{
    public class TheoryUILogic: ITheorySceneButton, IInitialization
    {
       
        
        private readonly Dictionary<LoadingPartsTheoryScene,Button> _buttonLogic;
        public event Action<LoadingPartsTheoryScene> LoadNext;
        
        public TheoryUILogic(Dictionary<LoadingPartsTheoryScene,Button> buttonLogic
        )
        {
            _buttonLogic = buttonLogic;
            
        }
        public void Initialization()
        {
            foreach (var button in _buttonLogic)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(()=> SwitchToNextMenu(button.Key));
            }
        }

        public void SwitchToNextMenu(LoadingPartsTheoryScene loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }

        
    }
}