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
    public class AuthInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _authParent;
        private readonly GameObject _authPrefab;
        private readonly AuthFactory _authFactory;
        private List<Button> _authButtons;
        private readonly AuthController _authController;

        public TMP_InputField Login;
        public TMP_InputField Password;

        public AuthInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject authParent, GameObject authPrefab, AuthController authController)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _authParent = authParent;
            _authPrefab = authPrefab;
            _authController = authController;
            _authFactory = new AuthFactory(_authPrefab);
        }

        public void Initialization()
        {
            var authMenu = _authFactory.Create(_authParent.transform);
            
            var bgVideo = authMenu.GetComponentInChildren<VideoPlayer>();
            bgVideo.renderMode = VideoRenderMode.CameraFarPlane;
            bgVideo.targetCamera = Camera.main;
            bgVideo.Play();
            
            authMenu.transform.localPosition = new Vector3(0,0,0);

            _authButtons = new List<Button>();
            _authButtons.AddRange(authMenu.GetComponentsInChildren<Button>());

            Login = authMenu.GetComponentsInChildren<TMP_InputField>()[0];
            Password = authMenu.GetComponentsInChildren<TMP_InputField>()[1];

            _authController.Login = Login;
            _authController.Password = Password;

            new AuthAddButtonsToDictionary(_authButtons, _gameContextWithViews);
            var authLogic = new AuthLogic(_gameContextWithViews.Buttons);
            authLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadAuth, authMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadAuth, authLogic);
        }
    }
}