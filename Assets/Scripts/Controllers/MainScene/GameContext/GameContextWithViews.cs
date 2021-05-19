using System.Collections.Generic;
using Diploma.Enums;
using Diploma.UI;
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
        public Dictionary<OptionsButtons, Button> OptionsButtons;
        public Dictionary<LoadingParts,GameObject> TextBoxesOnConstructor;
        public Dictionary<LoadingParts, Button> ErrorMenuButtons;
        public Dictionary<LoadingParts, Button> AboutButtons;
        public Dictionary<AssemblyCreating, Button> AssemblyCreatingButtons;
        
        public Dictionary<AnimatorButtons, Button> AnimatorButtons;
        public Dictionary<OptionsButtons,Slider> Sliders;
        public Dictionary<int,GameObject> ChoosenLessonToggles;
        public LessonChooseButtonsLogic LessonChooseButtonsLogic;

        public Dictionary<GameObject, Button> InventoryButtons;
        public GameContextWithViews()
        {
            ChoosenToggles = new Dictionary<int,GameObject>();
            MainMenuButtons = new Dictionary<LoadingParts, Button>();
            ChooseLessonButtons = new Dictionary<LoadingParts, Button>();
            AuthButtons = new Dictionary<LoadingParts, Button>();
            SignUpButtons = new Dictionary<LoadingParts, Button>();
            LessonConstructorButtons = new Dictionary<LoadingParts, Button>();
            OptionsButtons = new Dictionary<OptionsButtons, Button>();
            TextBoxesOnConstructor = new Dictionary<LoadingParts,GameObject>();
            ChoosenLessonToggles = new Dictionary<int, GameObject>();
            ErrorMenuButtons = new Dictionary<LoadingParts, Button>();
            AnimatorButtons = new Dictionary<AnimatorButtons, Button>();
            InventoryButtons = new Dictionary<GameObject, Button>();
            AboutButtons = new Dictionary<LoadingParts, Button>();
            Sliders = new Dictionary<OptionsButtons,Slider>();
            AssemblyCreatingButtons = new Dictionary<AssemblyCreating, Button>();
        }
        
        public void AddToggles(int id,GameObject toggle)
        {
            ChoosenToggles.Add(id,toggle);
        }
        
        public void SetLessonChooseButtonsLogic(LessonChooseButtonsLogic lessonChooseButtonsLogic)
        {
            LessonChooseButtonsLogic = lessonChooseButtonsLogic;
        }

        public void AddAssemblyCreatingMenuButtons(AssemblyCreating id, Button button)
        {
            if(!AssemblyCreatingButtons.ContainsKey(id))
                AssemblyCreatingButtons.Add(id,button);
        }
        public void SetSlider(OptionsButtons id,Slider slider)
        {
            if (!Sliders.ContainsKey(id))
                Sliders.Add(id,slider);
        }

        public void AddErrorMenuButtons(LoadingParts id, Button button)
        {
            if(!ErrorMenuButtons.ContainsKey(id))
                ErrorMenuButtons.Add(id,button);
        }
        public void AddAboutButtons(LoadingParts id, Button button)
        {
            if(!AboutButtons.ContainsKey(id))
                AboutButtons.Add(id,button);
        }
        public void AddAnimatorButtons(AnimatorButtons id, Button button)
        {
            if(!AnimatorButtons.ContainsKey(id))
                AnimatorButtons.Add(id,button);
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
        
        public void AddButtonInOptionsDictionary(OptionsButtons id, Button button)
        {
            if(!OptionsButtons.ContainsKey(id))
                OptionsButtons.Add(id,button);
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

        public void AddInventoryButtons(GameObject partOfAssembly, Button button)
        {
            if(!InventoryButtons.ContainsKey(partOfAssembly))
                InventoryButtons.Add(partOfAssembly, button);
        }
      
    }
}