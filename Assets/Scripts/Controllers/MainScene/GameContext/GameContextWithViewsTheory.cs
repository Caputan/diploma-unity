using System.Collections.Generic;
using Diploma.Enums;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class GameContextWithViewsTheory
    {
        public Dictionary<LoadingPartsTheoryScene, Button>  TheoryButtons;

        public GameContextWithViewsTheory()
        {
            TheoryButtons = new Dictionary<LoadingPartsTheoryScene, Button>();
        }
        
        public void AddTheoryButtonsButtons(LoadingPartsTheoryScene id, Button button)
        {
            if(!TheoryButtons.ContainsKey(id))
                TheoryButtons.Add(id,button);
        }
    }
}