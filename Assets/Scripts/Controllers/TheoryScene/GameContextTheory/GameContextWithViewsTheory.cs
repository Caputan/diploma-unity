using System.Collections.Generic;
using Diploma.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class GameContextWithViewsTheory
    {
        public Dictionary<LoadingPartsTheoryScene, Button>  TheoryButtons;
        public Dictionary<int, Button> LibraryButtons;
        //капелька хардкода.
        public List<Transform> Parents;
        public string nameOfFolder;

        public GameContextWithViewsTheory()
        {
            TheoryButtons = new Dictionary<LoadingPartsTheoryScene, Button>();
            LibraryButtons = new Dictionary<int, Button>();
            Parents = new List<Transform>();
        }

        public void SetNameOfFolder(string name)
        {
            nameOfFolder = name;
        }
        
        public void AddTheoryButtons(LoadingPartsTheoryScene id, Button button)
        {
            if(!TheoryButtons.ContainsKey(id))
                TheoryButtons.Add(id,button);
        }
        public void AddLibraryButtons(int id, Button button)
        {
            if(!LibraryButtons.ContainsKey(id))
                LibraryButtons.Add(id,button);
        }
        public void AddParentsToList(Transform transform)
        {
            Parents.Add(transform);
        }
    }
}