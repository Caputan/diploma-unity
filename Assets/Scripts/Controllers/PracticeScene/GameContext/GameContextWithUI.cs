using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;

namespace Diploma.PracticeScene.GameContext
{
    public sealed class GameContextWithUI: IGameContextUI
    {
        public Dictionary<PauseButtons, GameObject> UiControllers;
        public Dictionary<PauseButtons, IUIObject> UILogic;
        
        public GameContextWithUI()
        {
            UiControllers = new Dictionary<PauseButtons, GameObject>();
            UILogic = new Dictionary<PauseButtons, IUIObject>();
        }

        public void AddUILogic(PauseButtons loadingPart,IUIObject uiObject)
        {
            UILogic.Add(loadingPart,uiObject);
        }
        
        public void AddUIToDictionary(PauseButtons loadingPart, GameObject uiController)
        {
            if (!UiControllers.ContainsKey(loadingPart))
            {
                UiControllers.Add(loadingPart, uiController);
            }
        }
        
        public void RemoveUIFromDictionary(PauseButtons loadingPart, GameObject uiController)
        {
            if (UiControllers.ContainsKey(loadingPart))
            {
                UiControllers.Remove(loadingPart);
            }
        }
    }
}