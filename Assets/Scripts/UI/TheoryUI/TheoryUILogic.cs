using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace UI.TheoryUI
{
    public class TheoryUILogic: ITheorySceneButton, IInitialization, ICleanData
    {
        private readonly LoadingPartsTheoryScene _loadingPartsTheoryScene;
        private readonly Button _button;
        
        public event Action<LoadingPartsTheoryScene> LoadNext;
        
        public TheoryUILogic(LoadingPartsTheoryScene loadingPartsTheoryScene,Button button
        )
        {
            _loadingPartsTheoryScene = loadingPartsTheoryScene;
            _button = button;
            
        }
        public void Initialization()
        {
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(()=> SwitchToNextMenu(_loadingPartsTheoryScene));
        }

        public void SwitchToNextMenu(LoadingPartsTheoryScene loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }


        public void CleanData()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}