using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public sealed class GameContextWithViews
    {
        public Dictionary<int,GameObject> ChoosenToggles;
        public Dictionary<int, Button> Buttons;
        public Dictionary<int,GameObject> ChoosenLessonToggles;
        public GameObject infoPanel;
        
        public GameContextWithViews()
        {
            ChoosenToggles = new Dictionary<int,GameObject>();
            Buttons = new Dictionary<int, Button>();
        }
        
        public void AddToggles(int id,GameObject toggle)
        {
            ChoosenToggles.Add(id,toggle);
        }

        public void AddLessonsToggles(int id, GameObject toggle)
        {
            ChoosenLessonToggles.Add(id,toggle);
        }
        public void AddButtons(int id, Button button)
        {
            Buttons.Add(id,button);
        }

        public void SetInfoPanel(GameObject infopanel)
        {
            infoPanel = infopanel;
        }
      
    }
}