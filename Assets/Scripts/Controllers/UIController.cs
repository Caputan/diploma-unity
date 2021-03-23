using Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Diploma.Controllers
{
    public class UIController : IInitialization, ICleanData
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly ExitController _exitController;
        private readonly BackController _backController;
        private readonly AuthController _authController;
        private readonly FileManagerController _fileManagerController;
        private readonly LessonConstructorController _lessonConstructorController;
        private LoadingParts _currentPosition;

        public UIController(GameContextWithUI gameContextWithUI,
            ExitController exitController,
            BackController backController,
            AuthController authController,
            FileManagerController fileManagerController,
            LessonConstructorController lessonConstructorController
        )
        {
            _gameContextWithUI = gameContextWithUI;
            _exitController = exitController;
            _backController = backController;
            _authController = authController;
            _fileManagerController = fileManagerController;
            _lessonConstructorController = lessonConstructorController;
        }

        public void Initialization()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                var i =  value.Value;
                i.LoadNext += ShowUIByUIType;
            }
            HideAllUI();
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
                    GameObject.Find("BackGround").SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _gameContextWithUI.UiControllers[LoadingParts.LoadAuth].SetActive(true);
                    _currentPosition = LoadingParts.LoadStart;
                    break;
                case LoadingParts.LoadAuth:
                    //сюда еще к UI обращение надо
                    if (_authController.AddNewUser())
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadAuth].SetActive(true);
                        _currentPosition = LoadingParts.LoadAuth;
                    }
                    else
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        ShowUIByUIType(LoadingParts.LoadError);
                    }

                    break;
                case LoadingParts.LoadSignUp:
                    _gameContextWithUI.UiControllers[LoadingParts.LoadSignUp].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadSignUp;
                    break;
                case LoadingParts.LoadLectures:
                    _gameContextWithUI.UiControllers[LoadingParts.LoadLectures].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadLectures;
                    break;
                case LoadingParts.LoadCreationOfLesson:
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadCreationOfLesson;
                    break;
                case LoadingParts.Options:
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.Options;
                    break;
                case LoadingParts.LoadMain:
                    if (_authController.CheckAuthData())
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);
                        GameObject.Find("BackGround").SetActive(false);
                        _currentPosition = LoadingParts.LoadMain;
                    }
                    else
                    {
                        ShowUIByUIType(LoadingParts.LoadError);
                    }
                    break;
                case LoadingParts.Back:
                    ShowUIByUIType(_backController.GoBack());
                    break;
                case LoadingParts.LoadError:
                    _backController.WhereIMustBack(_currentPosition);
                    Debug.Log("Error!!");
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
                    _lessonConstructorController.CreateALesson();
                    _backController.WhereIMustBack(_currentPosition);
                    _gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);
                    _currentPosition = LoadingParts.LoadMain;
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
