using System;
using System.Collections.Generic;
using System.Diagnostics;
using Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

namespace Diploma.Controllers
{
    public class UIController : IInitialization, ICleanData
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly ExitController _exitController;
        private readonly BackController _backController;
        private LoadingParts _currentPosition;

        public UIController(GameContextWithUI gameContextWithUI,
            ExitController exitController,
            BackController backController
        )
        {
            _gameContextWithUI = gameContextWithUI;
            _exitController = exitController;
            _backController = backController;
        }

        public void Initialization()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                var i =  value.Value;
                i.LoadNext += ShowUIByUIType;
            }
            HideAllUI();
            ShowUIByUIType(LoadingParts.LoadAuth);
        }

        private void HideUI(GameObject Controller)
        {
            Controller.SetActive(false);
        }

        public void ShowUIByUIType(LoadingParts id)
        {
            HideAllUI();
            switch (id)
            {
                case LoadingParts.Exit:
                    _exitController.ExitApplication(); 
                    break;
                case LoadingParts.LoadAuth:
                    //сюда еще к UI обращение надо
                    _gameContextWithUI.UiControllers[LoadingParts.LoadAuth].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadAuth;
                    break;
                case LoadingParts.LoadSignUp:
                    _gameContextWithUI.UiControllers[LoadingParts.LoadSignUp].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadSignUp;
                    break;
                case LoadingParts.LoadLectures:
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadLectures;
                    break;
                case LoadingParts.LoadConstructor:
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadConstructor;
                    break;
                case LoadingParts.Options:
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.Options;
                    break;
                
                case LoadingParts.LoadMain:
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadMain;
                    break;
                case LoadingParts.Back:
                    ShowUIByUIType(_backController.GoBack());
                    break;
            }
            // нужно обращать к контроллеру
            //_gameContextWithUI.UiControllers[id].SetActive(true);
            Debug.Log(id);
        }

        public void HideUIByUIType(LoadingParts id)
        {
            
        }

        public void HideAllUI()
        {
            foreach (var controller in _gameContextWithUI.UiControllers)
            {
                HideUI(controller.Value);
            }
        }


        public void CleanData()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                var i = value.Value;
                i.LoadNext += ShowUIByUIType;
            }
        }
    }
}
