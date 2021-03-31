﻿using Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Diploma.Controllers
{
    public class UIController : IInitialization, ICleanData
    {
        //конечно такое себе использовать switch
        //можно было бы более простую State Machine прописать...
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly ExitController _exitController;
        private readonly BackController _backController;
        private readonly AuthController _authController;
        private readonly FileManagerController _fileManagerController;
        private readonly LessonConstructorController _lessonConstructorController;
        private readonly OptionsController _optionsController;
        private readonly LoadingSceneController _loadingSceneController;
        private readonly GameObject _backGround;
        private ErrorHandler _errorHandler;
        private LoadingParts _currentPosition;
        private ErrorCodes _error;


        public UIController(GameContextWithUI gameContextWithUI,
            ExitController exitController,
            BackController backController,
            AuthController authController,
            FileManagerController fileManagerController,
            LessonConstructorController lessonConstructorController,
            OptionsController optionsController,
            LoadingSceneController loadingSceneController
        )
        {
            _error = ErrorCodes.None;
            _gameContextWithUI = gameContextWithUI;
            _exitController = exitController;
            _backController = backController;
            _authController = authController;
            _fileManagerController = fileManagerController;
            _lessonConstructorController = lessonConstructorController;
            _optionsController = optionsController;
            _loadingSceneController = loadingSceneController;
            _backGround = GameObject.Find("BackGround");
            
        }

        public void Initialization()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                var i =  value.Value;
                i.LoadNext += ShowUIByUIType;
            }
            HideAllUI();
            _errorHandler = new ErrorHandler(_gameContextWithUI.UiControllers[LoadingParts.LoadError]);
            ShowUIByUIType(LoadingParts.LoadStart);
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
                case LoadingParts.LoadStart:
                    // _backGround.SetActive(true);
                    _authController.Login.text = "";
                    _authController.Password.text = "";
                    _backController.WhereIMustBack(_currentPosition);
                    _gameContextWithUI.UiControllers[LoadingParts.LoadAuth].SetActive(true);
                    _currentPosition = LoadingParts.LoadStart;
                    break;
                case LoadingParts.LoadAuth:
                    if (_authController.AddNewUser() == ErrorCodes.None)
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadAuth].SetActive(true);
                        _currentPosition = LoadingParts.LoadAuth;
                    }
                    else
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _error = _authController.AddNewUser();
                        ShowUIByUIType(LoadingParts.LoadError);
                        _currentPosition = LoadingParts.LoadStart;
                    }
                    break;
                case LoadingParts.LoadSignUp:
                    _authController.NewLogin.text = "";
                    _authController.NewPassword.text = "";
                    _authController.NewEmail.text = "";
                    _gameContextWithUI.UiControllers[LoadingParts.LoadSignUp].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadSignUp;
                    break;
                case LoadingParts.LoadLectures:
                    // _backGround.SetActive(false);
                    _gameContextWithUI.UiControllers[LoadingParts.LoadLectures].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadLectures;
                    break;
                case LoadingParts.LoadCreationOfLesson:
                    // _backGround.SetActive(false);
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadCreationOfLesson;
                    break;
                case LoadingParts.LoadMain:
                    if (_authController.CheckAuthData() == ErrorCodes.None)
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);
                        // _backGround.SetActive(false);
                        _currentPosition = LoadingParts.LoadMain;
                        _lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadModel,"Выберите деталь (*.3ds");
                        _lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadVideo,"Выберите видео-фаил (*.mp4)");
                        _lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadPDF,"Выберите текстовый фаил(*.pdf)");
                    }
                    else
                    {
                        _error = _authController.CheckAuthData();
                        ShowUIByUIType(LoadingParts.LoadError);
                    }
                    break;
                case LoadingParts.Back:
                    ShowUIByUIType(_backController.GoBack());
                    break;
                case LoadingParts.LoadError:
                    // _backGround.SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _errorHandler.ChangeErrorMessage(_error);
                    _gameContextWithUI.UiControllers[LoadingParts.LoadError].SetActive(true);
                    _currentPosition = LoadingParts.LoadError;
                    break;
                    // DownloadModel = 12,
                    // DownloadPDF = 13,
                    // DownloadVideo = 14,   
                case LoadingParts.DownloadModel:
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _fileManagerController.ShowSaveDialog(FileTypes.Assembly);
                    
                    break;
                case LoadingParts.DownloadPDF:
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _fileManagerController.ShowSaveDialog(FileTypes.Text);
                    
                    break;
                case LoadingParts.DownloadVideo:
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _fileManagerController.ShowSaveDialog(FileTypes.Video);
                    break;
                case LoadingParts.Next:
                    if (_fileManagerController.CheckForErrors() == ErrorCodes.None)
                    {
                        // _backGround.SetActive(false);
                        _lessonConstructorController.CreateALesson();
                        _backController.WhereIMustBack(_currentPosition);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);
                        _currentPosition = LoadingParts.LoadMain;
                    } else
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _error = _fileManagerController.CheckForErrors();
                        ShowUIByUIType(LoadingParts.LoadError);
                        _currentPosition = LoadingParts.LoadMain;
                    }

                    break;
                case LoadingParts.Options:
                    _backController.WhereIMustBack(_currentPosition);
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _currentPosition = LoadingParts.Options;
                    break;
                case LoadingParts.LowGraphics:
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _optionsController.SetGraphicsQuality(LoadingParts.LowGraphics);
                    _optionsController.DeactivateButton(LoadingParts.LowGraphics);
                    break;
                case LoadingParts.MiddleGraphics:
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _optionsController.SetGraphicsQuality(LoadingParts.MiddleGraphics);
                    _optionsController.DeactivateButton(LoadingParts.MiddleGraphics);
                    break;
                case LoadingParts.HighGraphics:
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _optionsController.SetGraphicsQuality(LoadingParts.HighGraphics);
                    _optionsController.DeactivateButton(LoadingParts.HighGraphics);
                    break;
                
            }
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
                i.LoadNext -= ShowUIByUIType;
            }
        }
    }
}
