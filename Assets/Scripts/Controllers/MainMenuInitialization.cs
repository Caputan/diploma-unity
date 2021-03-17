using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class MainMenuInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _mainParent;
        private readonly GameObject _prefabMain;
        private MainMenuFactory _mainMenuFactory;
        
        
        public MainMenuInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,GameObject MainParent, GameObject PrefabMain)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _prefabMain = PrefabMain;
            _mainMenuFactory = new MainMenuFactory(_prefabMain);
        }
        public void Initialization()
        {
            #region Main Menu Creation

            var MainMenu = _mainMenuFactory.Create(_mainParent.transform);
            MainMenu.transform.localPosition = new Vector3(0,0,0);
            var MainMenuLogic = new MainMenuLogic(_gameContextWithViews.Buttons);
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadMain, MainMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadMain,MainMenuLogic);
            
            #endregion
        }
    }
}