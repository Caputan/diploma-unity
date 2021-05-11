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
        public List<Transform> Parents;
        public Dictionary<int,string> nameOfFolders;
        public string urlVideo;

        public GameContextWithViewsTheory()
        {
            TheoryButtons = new Dictionary<LoadingPartsTheoryScene, Button>();
            LibraryButtons = new Dictionary<int, Button>();
            Parents = new List<Transform>();
            nameOfFolders = new Dictionary<int, string>();
            urlVideo = "-1";
        }

        public void SetNameOfFolder(int id,string name)
        {
            nameOfFolders.Add(id,name);
        }

        public void SetVideo(string url)
        {
            urlVideo = url;
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