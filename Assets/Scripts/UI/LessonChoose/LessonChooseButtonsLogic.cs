using System;
using System.Collections.Generic;
using Diploma.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class LessonChooseButtonsLogic: IInitialization
    {
        private readonly Dictionary<int, GameObject> _buttonLogic;

        public event Action<int> LoadLesson;
        
        public LessonChooseButtonsLogic(Dictionary<int,GameObject> buttonLogic)
        {
            _buttonLogic = buttonLogic;
        }


        public void Initialization()
        {
            foreach (var button in _buttonLogic)
            {
                button.Value.GetComponentInChildren<Button>().onClick.AddListener(()=> SwitchToLesson(button.Key));
            }
        }

        public void AddNewButton(int id)
        {
            _buttonLogic[id].GetComponentInChildren<Button>().onClick.AddListener(()=> SwitchToLesson(id));
        }

        public void SwitchToLesson(int lessonID)
        {
            LoadLesson?.Invoke(lessonID);
        }
    }
}