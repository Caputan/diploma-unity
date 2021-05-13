using System.Collections;
using Controllers;
using Coroutine;
using Controllers.MainScene.LessonsControllers;
using Data;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UI.LoadingUI;
using UnityEngine;
using UnityEngine.UI;

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
        private readonly OptionsController _optionsController;
        private readonly ScreenShotController _screenShotController;
        private readonly LoadingUILogic _loadingUILogic;
        private readonly ImportantDontDestroyData _importantDontDestroyData;
        private readonly GameObject _backGround;
        private ErrorHandler _errorHandler;
        private LoadingParts _currentPosition;
        private ErrorCodes _error;
        private Button[] _buttonMass;
        private Vector3[] _transforms = new Vector3[4];

        public UIController(GameContextWithUI gameContextWithUI,
            ExitController exitController,
            BackController backController,
            AuthController authController,
            FileManagerController fileManagerController,
            LessonConstructorController lessonConstructorController,
            OptionsController optionsController,
            ScreenShotController screenShotController,
            LoadingUILogic loadingUILogic,
            ImportantDontDestroyData importantDontDestroyData
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
            _screenShotController = screenShotController;
            _loadingUILogic = loadingUILogic;
            _importantDontDestroyData = importantDontDestroyData;

            _backGround = GameObject.Find("BackGround");
            
        }

        public void Initialization()
        {
            
            foreach (var value in _gameContextWithUI.UILogic)
            {
                if (value.Value is IMenuButton)
                {
                    Debug.Log("MainMenu "+value.Key);
                    var i = (IMenuButton) value.Value;
                    i.LoadNext += ShowUIByUIType;
                }

                if (value.Value is IUIOptions)
                {
                    Debug.Log("Options "+value.Key);
                    var i = (IUIOptions) value.Value;
                    i.LoadNext += ShowUIByUIType;
                }
                
            }
            _lessonConstructorController.TakeScreenShoot += TakeScreenShoot;
            HideAllUI();
            _buttonMass = _gameContextWithUI.UiControllers[LoadingParts.LoadMain].
                GetComponentsInChildren<Button>();
            for (int i=0;i<4;i++)
            {
                _transforms[i] = _buttonMass[i].transform.position;
            }
            _errorHandler = new ErrorHandler(_gameContextWithUI.UiControllers[LoadingParts.LoadError]);
            ShowUIByUIType(LoadingParts.LoadStart);
        }
        
        
        private void TakeScreenShoot(string obj)
        {
            HideAllUI();
            _backGround.SetActive(false);
            _screenShotController.TakeAScreanShoot(obj);
            WaitForTakingScreenShot().StartCoroutine(out _,out _);
            
        }

        private IEnumerator WaitForTakingScreenShot()
        {
            yield return new WaitForEndOfFrame();
            ShowUIByUIType(LoadingParts.LoadStart);
            _backGround.SetActive(true);
            yield return new WaitForSeconds(1);
            _lessonConstructorController.AddNewLessonToListOnUI();
        }

        private void HideUI(GameObject Controller)
        {
            Controller.SetActive(false);
        }

        public void ShowUIByUIType(OptionsButtons id)
        {
            switch (id)
            {
                case OptionsButtons.LowGraphics:
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _optionsController.SetGraphicsQuality(OptionsButtons.LowGraphics);
                    _optionsController.DeactivateButton(OptionsButtons.LowGraphics);
                    break;
                case OptionsButtons.MiddleGraphics:
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _optionsController.SetGraphicsQuality(OptionsButtons.MiddleGraphics);
                    _optionsController.DeactivateButton(OptionsButtons.MiddleGraphics);
                    break;
                case OptionsButtons.HighGraphics:
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _optionsController.SetGraphicsQuality(OptionsButtons.HighGraphics);
                    _optionsController.DeactivateButton(OptionsButtons.HighGraphics);
                    break;
                case OptionsButtons.Back:
                    ShowUIByUIType(_backController.GoBack());
                    break;
            }
        }
        public void ShowUIByUIType(LoadingParts id)
        {
            HideAllUI();
            switch (id)
            {
                case LoadingParts.Exit:
                    _importantDontDestroyData.activatedUserID = -1;
                    _importantDontDestroyData.lessonID = -1;
                    _exitController.ExitApplication(); 
                    break;
                case LoadingParts.LoadStart:
                    // _backGround.SetActive(true);
                    _loadingUILogic.SetActiveLoading(false);
                    //_authController.Login.text = "";
                    //_authController.Password.text = "";
                    // _importantDontDestroyData.activatedUserID = -1;
                    // _importantDontDestroyData.lessonID = -1;
                    _backController.WhereIMustBack(_currentPosition);
                    if (_importantDontDestroyData.activatedUserID == -1)
                    {
                        ShowUIByUIType(LoadingParts.ChangeUser);
                    }
                    else
                    {
                        _authController.ReopenUser(out int sameRole);
                        MainLoading(sameRole);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);
                    }
                    _currentPosition = LoadingParts.LoadStart;
                    break;
                case LoadingParts.ChangeUser:
                    _loadingUILogic.SetActiveLoading(false);
                    _authController.Login.text = "";
                    _authController.Password.text = "";
                    _importantDontDestroyData.activatedUserID = -1;
                    _importantDontDestroyData.lessonID = -1;
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
                    if (_authController.CheckAuthData(out var role) == ErrorCodes.None)
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);

                        MainLoading(role);
                    }
                    else
                    {
                        _error = _authController.CheckAuthData(out _);
                        ShowUIByUIType(LoadingParts.LoadError);
                    }
                    break;
                case LoadingParts.About:
                    _gameContextWithUI.UiControllers[LoadingParts.About].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.About;
                    break;
                case LoadingParts.Back:
                    ShowUIByUIType(_backController.GoBack());
                    break;
                case LoadingParts.LoadError:
                    _backController.WhereIMustBack(_currentPosition);
                    _errorHandler.ChangeErrorMessage(_error);
                    _gameContextWithUI.UiControllers[LoadingParts.LoadError].SetActive(true);
                    _currentPosition = LoadingParts.LoadError;
                    break;
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
                        if (_lessonConstructorController.CreateALesson())
                        {
                            _error = ErrorCodes.None;
                            //ShowUIByUIType(LoadingParts.LoadStart);
                        }
                        else
                        {
                            _backController.WhereIMustBack(_currentPosition);
                            ShowUIByUIType(LoadingParts.LoadError);
                            _currentPosition = LoadingParts.LoadStart;
                        }
                        //_backController.WhereIMustBack(_currentPosition);
                        //_gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);
                        //_currentPosition = LoadingParts.LoadMain;
                    } else
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _error = _fileManagerController.CheckForErrors();
                        ShowUIByUIType(LoadingParts.LoadError);
                        _currentPosition = LoadingParts.LoadStart;
                    }
                    break;
                case LoadingParts.Options:
                    _backController.WhereIMustBack(_currentPosition);
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _currentPosition = LoadingParts.Options;
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

        private void MainLoading(int role)
        {
            if (role == 2)
            {
                _buttonMass[0].gameObject.SetActive(false);
                _buttonMass[3].transform.position = 
                    _transforms[2];
                _buttonMass[2].transform.position = 
                    _transforms[0];
            }
            if (role == 1)
            {
                _buttonMass[0].gameObject.SetActive(true);
                _buttonMass[3].transform.position = 
                    _transforms[3];
                _buttonMass[2].transform.position = 
                    _transforms[2];
            }
            _currentPosition = LoadingParts.LoadMain;
            _lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadModel,"Выберите UnityBundle ()","");
            _lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadVideo,"Выберите видео-фаил (*.mp4)","");
            _lessonConstructorController.SetTextInTextBox(LoadingParts.DownloadPDF,"Выберите текстовый фаил(*.pdf)","");
        }
        
        public void CleanData()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                if (value.Value is IMenuButton)
                {
                    Debug.Log("MainMenu "+value.Key);
                    var i = (IMenuButton) value.Value;
                    i.LoadNext -= ShowUIByUIType;
                }
                if (value.Value is IUIOptions)
                {
                    Debug.Log("Options "+value.Key);
                    var i = (IUIOptions) value.Value;
                    i.LoadNext -= ShowUIByUIType;
                }
            }
            
            _lessonConstructorController.TakeScreenShoot -= TakeScreenShoot;
        }
    }
}
