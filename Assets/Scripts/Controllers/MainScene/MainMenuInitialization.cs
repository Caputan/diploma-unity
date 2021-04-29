using System.Collections.Generic;
using System.Linq;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public class MainMenuInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _mainParent;
        
        private MainMenuFactory _mainMenuFactory;
        private List<Button> MainMenuButtons;
        private AuthController _authController;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/MainMenu"};
        
        public MainMenuInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,GameObject MainParent, AuthController authController)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _authController = authController;
            _mainMenuFactory = new MainMenuFactory(ResourceLoader.LoadPrefab(_viewPath));
            
        }
        public void Initialization()
        {
            #region Main Menu Creation

            var MainMenu = _mainMenuFactory.Create(_mainParent.transform);
            MainMenu.transform.localPosition = new Vector3(0,0,0);
            
            MainMenuButtons = new List<Button>();
            MainMenuButtons.AddRange(MainMenu.GetComponentsInChildren<Button>());
            
            new MainMenuAddButtonsToDictionary(MainMenuButtons,_gameContextWithViews);

            var greetingsArray = MainMenu.GetComponentsInChildren<TextMeshProUGUI>();
            _authController.greetings = greetingsArray[greetingsArray.Length - 1];

            var MainMenuLogic = new MainMenuLogic(_gameContextWithViews.MainMenuButtons);
            MainMenuLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadMain, MainMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadMain,MainMenuLogic);
            
            #endregion
        }
    }
}