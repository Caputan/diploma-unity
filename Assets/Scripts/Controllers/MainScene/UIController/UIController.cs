using System;
using System.Collections;
using Controllers;
using Coroutine;
using Controllers.MainScene.LessonsControllers;
using Controllers.PracticeScene.PauseController;
using Data;
using Diploma.Controllers.AssembleController;
using Diploma.Enums;
using Diploma.Interfaces;
using DoTween;
using Interfaces;
using TMPro;
using UI.LoadingUI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Diploma.Controllers
{
    public class UIController : IInitialization, ICleanData, IExecute
    {
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly ExitController _exitController;
        private readonly BackController _backController;
        private readonly AuthController _authController;
        private readonly FileManagerController _fileManagerController;
        private readonly LessonConstructorController _lessonConstructorController;
        private readonly OptionsController _optionsController;
        private readonly ScreenShotController _screenShotController;
        private readonly LoadingUILogic _loadingUILogic;
        private readonly ImportantDontDestroyData _importantDontDestroyData;
        private readonly AssemblyCreator _assemblyCreatingController;
        private readonly PlayerInitialization _playerInitialization;
        private readonly GameObject _practiceScene;
        private readonly ErrorMenuInitialization _errorMenuInitialization;
        private readonly GameObject _backGround;
        private ErrorHandler _errorHandler;
        private LoadingParts _currentPosition;
        private ErrorCodes _error;
        private Button[] _buttonMass;
        private Vector3[] _transforms = new Vector3[4];
        private GameObject _gameObject;

        private GameObject _backGroundForCreatingOrder;
        public UIController(GameContextWithUI gameContextWithUI,
            GameContextWithLogic gameContextWithLogic,
            GameContextWithViews gameContextWithViews,
            ExitController exitController,
            BackController backController,
            AuthController authController,
            FileManagerController fileManagerController,
            LessonConstructorController lessonConstructorController,
            OptionsController optionsController,
            ScreenShotController screenShotController,
            LoadingUILogic loadingUILogic,
            ImportantDontDestroyData importantDontDestroyData,
            AssemblyCreator assemblyCreatingController,
            PlayerInitialization playerInitialization,
            GameObject practiceScene,
            ErrorMenuInitialization errorMenuInitialization
            )
        {
            _error = ErrorCodes.None;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithLogic = gameContextWithLogic;
            _gameContextWithViews = gameContextWithViews;
            _exitController = exitController;
            _backController = backController;
            _authController = authController;
            _fileManagerController = fileManagerController;
            _lessonConstructorController = lessonConstructorController;
            _optionsController = optionsController;
            _screenShotController = screenShotController;
            _loadingUILogic = loadingUILogic;
            _importantDontDestroyData = importantDontDestroyData;
            _assemblyCreatingController = assemblyCreatingController;
            _playerInitialization = playerInitialization;
            _practiceScene = practiceScene;
            _errorMenuInitialization = errorMenuInitialization;
            _gameObject = null;
            

            _backGround = GameObject.Find("BackGround");
            _backGround.SetActive(true);
           
        }

        private void GivingGameObj(GameObject obj)
        {
            _gameObject = obj;
        }

        public void Initialization()
        {
            
            foreach (var value in _gameContextWithUI.UILogic)
            {
                if (value.Value is IMenuButton)
                {
                    var i = (IMenuButton) value.Value;
                    i.LoadNext += ShowUIByUIType;
                }

                if (value.Value is IUIOptions)
                {
                    var i = (IUIOptions) value.Value;
                    i.LoadNext += ShowUIByUIType;
                }

                if (value.Value is ICreatingAssemblyButton)
                {
                    var i = (ICreatingAssemblyButton) value.Value;
                    i.LoadNext += ShowUIByUIType;
                }
                
            }
            _lessonConstructorController.GiveMeGameObject += GivingGameObj;
            _toggleEscape = false;
            _lessonConstructorController.TakeScreenShoot += TakeScreenShoot;
            _lessonConstructorController.TakeScreenShootOfPart += TakeAScreenShotOfPart;
            _backGroundForCreatingOrder = _gameContextWithUI.UiControllers[LoadingParts.CreateAssemblyDis]
                .transform.GetChild(0).gameObject;
            HideAllUI();
            _buttonMass = _gameContextWithUI.UiControllers[LoadingParts.LoadMain].
                GetComponentsInChildren<Button>();
            for (int i=0;i<4;i++)
            {
                _transforms[i] = _buttonMass[i].transform.position;
            }
            
            _errorHandler = new ErrorHandler(_errorMenuInitialization.Initialization());
            _errorHandler.MessageBoxBehaviour.UIController = this;
            ShowUIByUIType(LoadingParts.LoadStart);
        }

        private void TakeAScreenShotOfPart(string obj)
        {
            HideAllUI();
            _backGround.SetActive(false);
            _playerInitialization.TurnOnOffCamera(false,_gameContextWithLogic.MainCamera);
            _screenShotController.TakeAScreanShoot(obj);
        }


        private void TakeScreenShoot(string obj)
        {
            HideAllUI();
            _backGround.SetActive(false);
            _playerInitialization.TurnOnOffCamera(false,_gameContextWithLogic.MainCamera);
            _screenShotController.TakeAScreanShoot(obj);
            WaitForTakingScreenShot().StartCoroutine(out _,out _);
            
            
        }
        private IEnumerator WaitForTakingScreenShotParts()
        {
            yield return new WaitForEndOfFrame();
            yield break;
        }

        private IEnumerator WaitForTakingScreenShot()
        {
            yield return new WaitForEndOfFrame();
            ShowUIByUIType(LoadingParts.LoadStart);
            _backGround.SetActive(true);
            yield return new WaitForSeconds(1);
            _lessonConstructorController.AddNewLessonToListOnUI();
            yield break;
        }

        private void HideUI(GameObject Controller)
        {
            Controller.SetActive(false);
        }

        private void ShowUIByUIType(AssemblyCreating id)
        {
            switch (id)
            {
                case AssemblyCreating.Decline:
                    _lessonConstructorController.DestroyAssembly();
                    ShowUIByUIType(LoadingParts.Back);
                    break;
                case AssemblyCreating.SavingOrder:
                    _assemblyCreatingController.EndCreating();
                    ShowUIByUIType(LoadingParts.Next);
                    break;
                case AssemblyCreating.GoBackOnePart:
                    //_gameContextWithUI.UiControllers[LoadingParts.CreateAssemblyDis].SetActive(true);
                    _assemblyCreatingController.BackInOrder();
                    break;
            }
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
            Debug.Log(id);
            switch (id)
            {
                case LoadingParts.Exit:
                    _importantDontDestroyData.activatedUserID = -1;
                    _importantDontDestroyData.lessonID = -1;
                    _exitController.ExitApplication(); 
                    break;
                case LoadingParts.LoadStart:
                    HideAllUI();
                    _loadingUILogic.SetActiveLoading(false);
                    _playerInitialization.SetPause(true);
                    _playerInitialization.TurnOnOffCamera(false,_gameContextWithLogic.MainCamera);
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
                    HideAllUI();
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
                    HideAllUI();
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
                    HideAllUI();
                    _authController.NewLogin.text = "";
                    _authController.NewPassword.text = "";
                    _authController.NewEmail.text = "";
                    _gameContextWithUI.UiControllers[LoadingParts.LoadSignUp].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadSignUp;
                    break;
                case LoadingParts.LoadLectures:
                    HideAllUI();
                    _gameContextWithUI.UiControllers[LoadingParts.LoadLectures].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadLectures;
                    break;
                case LoadingParts.LoadCreationOfLesson:
                    HideAllUI();
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _backController.WhereIMustBack(_currentPosition);
                    _currentPosition = LoadingParts.LoadCreationOfLesson;
                    // очищать тоглы и имя
                    
                    _lessonConstructorController._playerChoose = false;
                    break;
                case LoadingParts.LoadMain:
                    HideAllUI();
                    _practiceScene.SetActive(true);
                    if (_authController.CheckAuthData(out var role, out var marks) == ErrorCodes.None)
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _gameContextWithUI.UiControllers[LoadingParts.LoadMain].SetActive(true);
                        SetMarks(marks);
                        MainLoading(role);
                    }
                    else
                    {
                        _error = _authController.CheckAuthData(out _, out _);
                        ShowUIByUIType(LoadingParts.LoadError);
                    }
                    break;
                case LoadingParts.About:
                    HideAllUI();
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
                    _errorHandler.MessageBoxBehaviour.Show();
                    break;
                case LoadingParts.UnLoadError:
                    ShowUIByUIType(LoadingParts.Back);
                    break;
                case LoadingParts.DownloadModel:
                    HideAllUI();
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _fileManagerController.ShowSaveDialog(FileTypes.Assembly);
                    
                    break;
                case LoadingParts.DownloadPDF:
                    HideAllUI();
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _fileManagerController.ShowSaveDialog(FileTypes.Text);
                    
                    break;
                case LoadingParts.DownloadVideo:
                    HideAllUI();
                    _gameContextWithUI.UiControllers[LoadingParts.LoadCreationOfLesson].SetActive(true);
                    _fileManagerController.ShowSaveDialog(FileTypes.Video);
                    break;
                case LoadingParts.CreateAssemblyDis:
                    HideAllUI();
                    CreateAssemblyDisPause().StartCoroutine(out _, out _);
                    break;
                case LoadingParts.Next:
                    HideAllUI();
                    if (_fileManagerController.CheckForErrors() == ErrorCodes.None)
                    {
                        if (_lessonConstructorController.CreateALesson())
                        {
                            _error = ErrorCodes.None;
                            
                        }
                        else
                        {
                            _backController.WhereIMustBack(_currentPosition);
                            ShowUIByUIType(LoadingParts.LoadError);
                            _currentPosition = LoadingParts.LoadStart;
                            
                        }
                    } else
                    {
                        _backController.WhereIMustBack(_currentPosition);
                        _error = _fileManagerController.CheckForErrors();
                        ShowUIByUIType(LoadingParts.LoadError);
                        _currentPosition = LoadingParts.LoadStart;
                    }
                    
                    break;
                case LoadingParts.Options:
                    HideAllUI();
                    _backController.WhereIMustBack(_currentPosition);
                    _gameContextWithUI.UiControllers[LoadingParts.Options].SetActive(true);
                    _currentPosition = LoadingParts.Options;
                    break;
            }
        }

        public void HideUIByUIType(LoadingParts id)
        {
            
        }

        public void HideAllUI()
        {
            foreach (var controller in _gameContextWithUI.UiControllers)
            {
                if (controller.Key!=LoadingParts.LoadError)
                {
                    HideUI(controller.Value);
                }
            }
        }

        public void SetMarks(string marks)
        {
            var marksInt = marks.Split(' ');
            foreach (var button in _gameContextWithViews.LessonChooseButtonsLogic._buttonLogic)
            {
                foreach (var mark in marksInt)
                {
                    if (button.Key == Convert.ToInt32(mark))
                    {
                        button.Value.transform.GetChild(3).gameObject.SetActive(true);
                    }
                }
            }
        }
        
        private IEnumerator CreateAssemblyDisPause()
        {
            _gameObject = null;
            _loadingUILogic.SetActiveLoading(true);
            yield return new WaitForFixedUpdate();
            _playerInitialization.SetPause(false);
            _playerInitialization.TurnOnOffCamera(true,_gameContextWithLogic.MainCamera);
            yield return new WaitForFixedUpdate();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            yield return new WaitForFixedUpdate();
            _lessonConstructorController.OpenAnUIInitialization();
            _loadingUILogic.LoadingParams("Ожидайте,пожалуйста","Загрузка");
            yield return new WaitUntil(()=> _gameObject != null);
            if (_lessonConstructorController.CheckForErrors() == ErrorCodes.None)
            {
                _assemblyCreatingController.SetAssemblyGameObject(_gameObject,
                    _gameContextWithUI.UiControllers[LoadingParts.CreateAssemblyDis].
                        transform.GetChild(2).GetChild(0).GetChild(0).gameObject); 
            }
            else
            {
                _currentPosition = LoadingParts.LoadCreationOfLesson;
                _playerInitialization.SetPause(true);
                _playerInitialization.TurnOnOffCamera(false,_gameContextWithLogic.MainCamera);
                ShowUIByUIType(LoadingParts.LoadError);
            }
            yield return new WaitForEndOfFrame();
            _loadingUILogic.SetActiveLoading(false);
            _gameContextWithUI.UiControllers[LoadingParts.CreateAssemblyDis].SetActive(true);
            yield break;
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
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].GetComponent<TMP_InputField>().text = "";
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].GetComponentInChildren<TextMeshProUGUI>().text = "Введите название урока";
            var whereObject =
                _gameContextWithUI.UiControllers[LoadingParts.CreateAssemblyDis].transform.GetChild(2).GetChild(0)
                    .GetChild(0).gameObject;
            for (int i = 0; i < whereObject.transform.childCount; i++)
            {
                Object.Destroy(whereObject.transform.GetChild(i).gameObject);
            }
            _backGroundForCreatingOrder.SetActive(false);
            _toggleEscape = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            foreach (var toggle in _gameContextWithViews.ChoosenToggles)
            {
                toggle.Value.GetComponentInChildren<Toggle>().isOn = false;
            }
        }
        
        public void CleanData()
        {
            foreach (var value in _gameContextWithUI.UILogic)
            {
                if (value.Value is IMenuButton)
                {
                    var i = (IMenuButton) value.Value;
                    i.LoadNext -= ShowUIByUIType;
                }
                if (value.Value is IUIOptions)
                {
                    var i = (IUIOptions) value.Value;
                    i.LoadNext -= ShowUIByUIType;
                }
            }
            _lessonConstructorController.GiveMeGameObject -= GivingGameObj;
            _lessonConstructorController.TakeScreenShoot -= TakeScreenShoot;
        }

        private bool _toggleEscape;
        public void Execute(float deltaTime)
        {
            if (Input.GetKeyDown (KeyCode.Escape))
            {
                _toggleEscape = !_toggleEscape;
                _playerInitialization.SetPause(_toggleEscape);
                _backGroundForCreatingOrder.SetActive(!_backGroundForCreatingOrder.activeSelf);
                if (_toggleEscape)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }

            if (_gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadModel].
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text != "Выберите UnityBundle ()" &&
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.DownloadPDF].
                transform.GetChild(0).GetComponent<TextMeshProUGUI>().text != "Выберите текстовый фаил(*.pdf)" &&
            _gameContextWithViews.TextBoxesOnConstructor[LoadingParts.SetNameToLesson].
                GetComponent<TMP_InputField>().text != "" && TogglesAreNotZero()
            )
            {
                _gameContextWithViews.LessonConstructorButtons[LoadingParts.CreateAssemblyDis].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                _gameContextWithViews.LessonConstructorButtons[LoadingParts.CreateAssemblyDis].enabled = true;
            }
            else
            {
                _gameContextWithViews.LessonConstructorButtons[LoadingParts.CreateAssemblyDis].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                _gameContextWithViews.LessonConstructorButtons[LoadingParts.CreateAssemblyDis].enabled = false;
            }

            if (_gameContextWithUI.UiControllers[LoadingParts.CreateAssemblyDis].
                transform.GetChild(2).GetChild(0).GetChild(0).gameObject.transform.childCount != 0)
            {
                _gameContextWithViews.AssemblyCreatingButtons[AssemblyCreating.SavingOrder].GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
                _gameContextWithViews.AssemblyCreatingButtons[AssemblyCreating.SavingOrder].enabled = true;
            }
            else
            {
                _gameContextWithViews.AssemblyCreatingButtons[AssemblyCreating.SavingOrder].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                _gameContextWithViews.AssemblyCreatingButtons[AssemblyCreating.SavingOrder].enabled = false;
            }
        }

        private bool TogglesAreNotZero()
        {
            foreach (var toggle in _gameContextWithViews.ChoosenToggles)
            {
                if (toggle.Value.GetComponentInChildren<Toggle>().isOn)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
