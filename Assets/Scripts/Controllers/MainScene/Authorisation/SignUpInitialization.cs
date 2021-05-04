using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Diploma.Controllers
{
    public class SignUpInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _mainParent;
        private readonly SignUpFactory _signUpFactory;
        private List<Button> SignUpButtons;
        private AuthController _authController;
        
        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/Registration"};
        
        public TMP_InputField Login;
        public  TMP_InputField Password;
        public  TMP_InputField Email;
        private TMP_Dropdown Role;

        public SignUpInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject MainParent, AuthController authController)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _authController = authController;
            _signUpFactory = new SignUpFactory(ResourceLoader.LoadPrefab(_viewPath));
        }

        public void Initialization()
        {
            var signUpMenu = _signUpFactory.Create(_mainParent.transform);

            signUpMenu.transform.localPosition = new Vector3(0,0,0);

            SignUpButtons = new List<Button>();
            SignUpButtons.AddRange(signUpMenu.GetComponentsInChildren<Button>());
            var buff = signUpMenu.GetComponentsInChildren<TMP_InputField>();
            Login = buff[0];
            Password = buff[1];
            Email = buff[2];
            Role = signUpMenu.GetComponentInChildren<TMP_Dropdown>();
            
            _authController.NewLogin = Login;
            _authController.NewPassword = Password;
            _authController.NewEmail = Email;
            _authController.Role = Role;
            
            new SignUpAddButtonsToDictionary(SignUpButtons, _gameContextWithViews);
            var signUpLogic = new SignUpLogic(_gameContextWithViews.SignUpButtons);
            signUpLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadSignUp, signUpMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadSignUp, signUpLogic);
        }
    }
}