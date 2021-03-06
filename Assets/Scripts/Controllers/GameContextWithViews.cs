using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class GameContextWithViews
    {
        public Dictionary<int,GameObject> ChoosenToggles;
        public Dictionary<int, Button> Buttons;
        
        public GameContextWithViews()
        {
            ChoosenToggles = new Dictionary<int,GameObject>();
            Buttons = new Dictionary<int, Button>();
        }
        
        public void AddToggles(int id,GameObject toggle)
        {
            ChoosenToggles.Add(id,toggle);
        }

        public void AddButtons(int id, Button button)
        {
            Buttons.Add(id,button);
        }
    }
}