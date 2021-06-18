using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using DoTween;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class ErrorMenuInitialization: ICleanData
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _mainParent;
        private GameObject _errorMenu;
        private readonly ErrorMenuFactory _errorMenuFactory;
        private List<Button> ErrorMenuButtons;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/ErrorMenu"};
        
        public ErrorMenuInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject MainParent)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _errorMenuFactory = new ErrorMenuFactory(ResourceLoader.LoadPrefab(_viewPath));
        }

        public GameObject Initialization()
        {
            _errorMenu = _errorMenuFactory.Create(_mainParent.transform);
            
            _errorMenu.transform.localPosition = new Vector3(0,0,0);
            
            ErrorMenuButtons = new List<Button>();
            ErrorMenuButtons.AddRange(_errorMenu.GetComponentsInChildren<Button>());

            //var errorHandler = new ErrorHandler(_errorMenu);

            new ErrorMenuAddButtonsToDictionary(ErrorMenuButtons, _gameContextWithViews);
            var errorMenuLogic = new ErrorMenuLogic(_gameContextWithViews.ErrorMenuButtons);
            //errorMenuLogic.Initialization();
            
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadError, _errorMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadError, errorMenuLogic);
            return _errorMenu;
        }


        public void CleanData()
        {
            Object.Destroy(_errorMenu);
        }
    }
}