using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ErrorMenuInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _mainParent;
        private readonly GameObject _errorMenuPrefab;
        private readonly ErrorMenuFactory _errorMenuFactory;
        private List<Button> ErrorMenuButtons;

        public ErrorMenuInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject MainParent, GameObject errorMenuPrefab)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _errorMenuPrefab = errorMenuPrefab;
            _errorMenuFactory = new ErrorMenuFactory(_errorMenuPrefab);
        }

        public void Initialization()
        {
            var errorMenu = _errorMenuFactory.Create(_mainParent.transform);
            
            errorMenu.transform.localPosition = new Vector3(0,0,0);
            
            ErrorMenuButtons = new List<Button>();
            ErrorMenuButtons.AddRange(errorMenu.GetComponentsInChildren<Button>());

            var errorHandler = new ErrorHandler(errorMenu);

            new ErrorMenuAddButtonsToDictionary(ErrorMenuButtons, _gameContextWithViews);
            var errorMenuLogic = new ErrorMenuLogic(_gameContextWithViews.ErrorMenuButtons);
            errorMenuLogic.Initialization();
            
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadError, errorMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadError, errorMenuLogic);
        }
    }
}