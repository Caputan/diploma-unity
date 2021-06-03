using System.Collections.Generic;
using Diploma.Enums;
using UnityEngine.UI;

namespace Diploma.PracticeScene.GameContext
{
    public sealed class GameContextWithView
    {

        public Dictionary<PauseButtons, Button> PauseButtons;
        public Dictionary<CompleteButtons, Button> CompleteButtons;
        
        public GameContextWithView()
        {
            PauseButtons = new Dictionary<PauseButtons, Button>();
            CompleteButtons = new Dictionary<CompleteButtons, Button>();
        }

        public void AddPauseButtons(PauseButtons id, Button button)
        {
            if(!PauseButtons.ContainsKey(id))
                PauseButtons.Add(id, button);
        }
        
        public void AddCompleteButtons(CompleteButtons id, Button button)
        {
            if(!CompleteButtons.ContainsKey(id))
                CompleteButtons.Add(id, button);
        }
    }
}