using System;
using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class LessonChooseLogic: IUILoadLessonChoose, IInitialization
    {
        public event Action<int> LoadNext;
        
        private readonly Dictionary<int,Button> _buttonLogic;
        private readonly Dictionary<int,int> _lessonIDPlusButtonID;
        
        public LessonChooseLogic(Dictionary<int,Button> buttonLogic)
        {
            _buttonLogic = buttonLogic;
            
            //_lessonIDPlusButtonID
        }
        public void Initialization()
        {
            foreach (var button in _buttonLogic)
            {
                button.Value.onClick.RemoveAllListeners();
                button.Value.onClick.AddListener(()=> SwitchToNextMenu(_lessonIDPlusButtonID[button.Key]));
            }
        }
        
        public void SwitchToNextMenu(int id)
        {
            LoadNext.Invoke(id);
        }
    }
}