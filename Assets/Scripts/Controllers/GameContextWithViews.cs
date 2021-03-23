using System.Collections.Generic;
using Diploma.Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public sealed class GameContextWithViews
    {
        public Dictionary<int,GameObject> ChoosenToggles;
        public Dictionary<LoadingParts, Button>  MainMenuButtons;
        public Dictionary<LoadingParts, Button>  ChooseLessonButtons;
        public Dictionary<LoadingParts, Button>  AuthButtons;
        public Dictionary<LoadingParts, Button>  SignUpButtons;
        public Dictionary<LoadingParts, Button>  LessonConstructorButtons;
        public Dictionary<LoadingParts,GameObject> TextBoxesOnConstructor;
        
        public Dictionary<int,GameObject> ChoosenLessonToggles;
        public GameContextWithViews()
        {
            ChoosenToggles = new Dictionary<int,GameObject>();
            MainMenuButtons = new Dictionary<LoadingParts, Button>();
            ChooseLessonButtons = new Dictionary<LoadingParts, Button>();
            AuthButtons = new Dictionary<LoadingParts, Button>();
            SignUpButtons = new Dictionary<LoadingParts, Button>();
            ChoosenLessonToggles = new Dictionary<int, GameObject>();
            LessonConstructorButtons = new Dictionary<LoadingParts, Button>();
            TextBoxesOnConstructor = new Dictionary<LoadingParts,GameObject>();
        }
        
        public void AddToggles(int id,GameObject toggle)
        {
            ChoosenToggles.Add(id,toggle);
        }

        public void AddTextBoxesToListInConstructor(LoadingParts loadingParts,GameObject gameObject)
        {
            if(!TextBoxesOnConstructor.ContainsKey(loadingParts))
                TextBoxesOnConstructor.Add(loadingParts,gameObject);
        }

        public void AddLessonsToggles(int id, GameObject toggle)
        {
            ChoosenLessonToggles.Add(id,toggle);
        }
        public void AddMainMenuButtons(LoadingParts id, Button button)
        {
            if(!MainMenuButtons.ContainsKey(id))
                MainMenuButtons.Add(id,button);
        }
        public void AddChooseLessonButtons(LoadingParts id, Button button)
        {
            if(!ChooseLessonButtons.ContainsKey(id))
                ChooseLessonButtons.Add(id,button);
        }
        public void AddAuthButtons(LoadingParts id, Button button)
        {
            if(!AuthButtons.ContainsKey(id))
                AuthButtons.Add(id,button);
        }
        public void AddSignUpButtons(LoadingParts id, Button button)
        {
            if(!SignUpButtons.ContainsKey(id))
                SignUpButtons.Add(id,button);
        }

        public void AddLessonConstructorButtons(LoadingParts id, Button button)
        {
            if(!LessonConstructorButtons.ContainsKey(id))
                LessonConstructorButtons.Add(id,button);
            
        }
      
    }
}