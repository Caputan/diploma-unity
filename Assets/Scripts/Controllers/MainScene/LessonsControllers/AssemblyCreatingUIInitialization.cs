using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using Tools;
using UI.CreatingAssemblyUI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.MainScene.LessonsControllers
{
    public sealed class AssemblyCreatingUIInitialization: IInitialization
    {
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/CreationDis"};
        private readonly CreatingAssemblyFactory _creatingAssemblyFactory;
        private readonly GameObject _canvas;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private List<Button> AssemblyCreationButtons;
        
        public AssemblyCreatingUIInitialization(
            GameObject Canvas,
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI
            )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            
            _canvas = Canvas;
            
            _creatingAssemblyFactory = new CreatingAssemblyFactory(ResourceLoader.LoadPrefab(_viewPath));
        }

        public void Initialization()
        {
            #region Lesson constructor Creation

            var Constructor = _creatingAssemblyFactory.Create(_canvas.transform);
            Constructor.transform.localPosition = new Vector3(0,0,0);

            AssemblyCreationButtons = new List<Button>();
            AssemblyCreationButtons.AddRange(Constructor.GetComponentsInChildren<Button>());
            
            var CreatingAssemblyUIAddButtonsToDictionary = new CreatingAssemblyAddButtonsToDictionary(
                AssemblyCreationButtons,_gameContextWithViews
            );
            var ConstructorLogic = new CreatingAssemblyLogic(_gameContextWithViews.AssemblyCreatingButtons);
            ConstructorLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.CreateAssemblyDis, Constructor);
            _gameContextWithUI.AddUILogic(LoadingParts.CreateAssemblyDis,ConstructorLogic);
            #endregion
        }

       
    }
}