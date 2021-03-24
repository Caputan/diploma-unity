using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using TMPro;
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
        private readonly GameObject _signUpPrefab;
        private readonly SignUpFactory _signUpFactory;
        private List<Button> SignUpButtons;

        private AuthController _authController;
        public TMP_InputField Login;
        public  TMP_InputField Password;
        public  TMP_InputField Email;

        public SignUpInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject MainParent, GameObject signUpPrefab, AuthController authController)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _signUpPrefab = signUpPrefab;
            _authController = authController;
            _signUpFactory = new SignUpFactory(_signUpPrefab);
        }

        public void Initialization()
        {
            var signUpMenu = _signUpFactory.Create(_mainParent.transform);
            
            // var bgVideo = signUpMenu.GetComponentInChildren<VideoPlayer>();
            // bgVideo.renderMode = VideoRenderMode.CameraFarPlane;
            // bgVideo.targetCamera = Camera.main;
            // bgVideo.Play();
            
            signUpMenu.transform.localPosition = new Vector3(0,0,0);

            SignUpButtons = new List<Button>();
            SignUpButtons.AddRange(signUpMenu.GetComponentsInChildren<Button>());
            
            Login = signUpMenu.GetComponentsInChildren<TMP_InputField>()[0];
            Password = signUpMenu.GetComponentsInChildren<TMP_InputField>()[1];
            Email = signUpMenu.GetComponentsInChildren<TMP_InputField>()[2];

            _authController.NewLogin = Login;
            _authController.NewPassword = Password;
            _authController.NewEmail = Email;

            new SignUpAddButtonsToDictionary(SignUpButtons, _gameContextWithViews);
            var signUpLogic = new SignUpLogic(_gameContextWithViews.SignUpButtons);
            signUpLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadSignUp, signUpMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadSignUp, signUpLogic);
        }
    }
}