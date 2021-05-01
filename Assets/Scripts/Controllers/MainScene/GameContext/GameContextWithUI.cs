using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;

namespace Diploma.Controllers
{
    public class GameContextWithUI: IGameContextUI
    {
        public Dictionary<LoadingParts, GameObject> UiControllers;
        public Dictionary<OptionsButtons, GameObject> UIControllersOptions;
        public Dictionary<LoadingParts, IUIObject> UILogic;
        
        public GameContextWithUI()
        {
            UiControllers = new Dictionary<LoadingParts, GameObject>();
            UILogic = new Dictionary<LoadingParts, IUIObject>();
        }

        public void AddUILogic(LoadingParts loadingPart,IUIObject uiObject)
        {
            UILogic.Add(loadingPart,uiObject);
        }
        
        public void AddUIToDictionary(LoadingParts loadingPart, GameObject uiController)
        {
            if (!UiControllers.ContainsKey(loadingPart))
            {
                UiControllers.Add(loadingPart, uiController);
            }
        }
        public void AddUIOptionsToDictionary(OptionsButtons loadingPart, GameObject uiController)
        {
            if (!UIControllersOptions.ContainsKey(loadingPart))
            {
                UIControllersOptions.Add(loadingPart, uiController);
            }
        }
        
        public void RemoveUIFromDictionary(LoadingParts loadingPart, GameObject uiController)
        {
            if (UiControllers.ContainsKey(loadingPart))
            {
                UiControllers.Remove(loadingPart);
            }
        }
    }
}