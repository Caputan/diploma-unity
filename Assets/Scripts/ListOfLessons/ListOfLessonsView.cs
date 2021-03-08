using System;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine.UI;

namespace ListOfLessons
{
    public sealed class ListOfLessonsView: IInitialization
    {
        public ListOfLessonsView()
        {
            
        }
        public void Initialization()
        {
            
            // foreach (var toggle in _gameContextWithViews.ChoosenToggles)
            // {
            //     toggle.Value.GetComponentInChildren<Toggle>().onValueChanged.AddListener(on=>
            //     {
            //         if (on) _toggleID = toggle.Value.GetInstanceID();
            //     });
            //     
            //     toggle.Value.SetActive(false);
            // }
            //
            // _button.onClick.RemoveAllListeners();
            // _button.onClick.AddListener(() => ChoosedNextStage());
        }
        
        public void ChooseLessionFromList()
        {
            //ChooseLession.Invoke();
        }
        
        public event Action<int> ChooseLession;
        
    }
}