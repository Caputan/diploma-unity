using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class GameContextWithViews
    {
        public Dictionary<int,GameObject> ChoosenToggles;

        public GameContextWithViews()
        {
            ChoosenToggles = new Dictionary<int,GameObject>();
        }
        
        public void AddToggles(int id,GameObject toggle)
        {
            ChoosenToggles.Add(id,toggle);
        }
    }
}