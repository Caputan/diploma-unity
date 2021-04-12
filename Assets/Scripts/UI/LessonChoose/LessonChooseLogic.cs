using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class LessonChooseLogic: IMenuButton, IInitialization
    {
        public event Action<LoadingParts> LoadNext;

        private readonly Dictionary<LoadingParts,Button> _buttonLogic;
        private readonly Dictionary<int,int> _lessonIDPlusButtonID;
        
        public LessonChooseLogic(Dictionary<LoadingParts,Button> buttonLogic)
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
        
        public void SwitchToNextMenu(LoadingParts loadingParts)
        {
            LoadNext?.Invoke(loadingParts);
        }
    }
}