using System;
using Diploma.Interfaces;

namespace ListOfLessons
{
    public sealed class ListOfLessonsViewCreator: IInitialization
    {
        public ListOfLessonsViewCreator()
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
    }
}