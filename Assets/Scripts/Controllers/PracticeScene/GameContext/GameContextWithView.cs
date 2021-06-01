using System.Collections.Generic;
using Diploma.Enums;
using UnityEngine.UI;

namespace Diploma.PracticeScene.GameContext
{
    public sealed class GameContextWithView
    {

        public Dictionary<PauseButtons, Button> PauseButtons;
        
        public GameContextWithView()
        {
            PauseButtons = new Dictionary<PauseButtons, Button>();
        }

        public void AddPauseButtons(PauseButtons id, Button button)
        {
            if(!PauseButtons.ContainsKey(id))
                PauseButtons.Add(id, button);
        }
    }
}