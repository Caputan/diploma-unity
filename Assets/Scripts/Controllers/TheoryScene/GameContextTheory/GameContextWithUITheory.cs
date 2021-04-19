using System.Collections.Generic;
using Diploma.Enums;
using Interfaces;
using UnityEngine;

namespace Diploma.Controllers
{
    public sealed class GameContextWithUITheory
    {
        public Dictionary<int, IUIObject> UITreeLogic;
        public Dictionary<LoadingPartsTheoryScene, IUIObject> UILogic;
        public Dictionary<LoadingPartsTheoryScene, GameObject> UiControllers;
        
        public GameContextWithUITheory()
        {
            UITreeLogic = new Dictionary<int, IUIObject>();
            UILogic = new Dictionary<LoadingPartsTheoryScene, IUIObject>();
            UiControllers = new Dictionary<LoadingPartsTheoryScene, GameObject>();
        }

        public void AddUILogic(LoadingPartsTheoryScene loadingPart,IUIObject uiObject)
        {
            if (!UILogic.ContainsKey(loadingPart))
            {
                UILogic.Add(loadingPart, uiObject);
            }
        }

        public void AddUIController(LoadingPartsTheoryScene loadingPart, GameObject gameObject)
        {
            if (!UiControllers.ContainsKey(loadingPart))
            {
                UiControllers.Add(loadingPart, gameObject);
            }
        }
        
        public void AddUITreeLogic(int id, IUIObject uiObject)
        {
            if (!UITreeLogic.ContainsKey(id))
            {
                UITreeLogic.Add(id, uiObject);
            }
        }
    }
}