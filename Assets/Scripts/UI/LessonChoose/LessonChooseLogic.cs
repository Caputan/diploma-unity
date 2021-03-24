using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class LessonChooseLogic: IUIObject, IInitialization
    {
        public event Action<LoadingParts> LoadNext;

        private readonly Dictionary<LoadingParts,Button> _buttonLogic;
        private readonly Dictionary<int,int> _lessonIDPlusButtonID;
        
        public LessonChooseLogic(Dictionary<LoadingParts,Button> buttonLogic)
        {
            _buttonLogic = buttonLogic;
            // нужно понять как передавать номер урока, который требуется загрузить на следующую сцену
            //_lessonIDPlusButtonID
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
            LoadNext.Invoke(loadingParts);
        }
    }
}