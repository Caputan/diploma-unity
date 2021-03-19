using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
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

        public SignUpInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject MainParent, GameObject signUpPrefab)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _mainParent = MainParent;
            _signUpPrefab = signUpPrefab;
            _signUpFactory = new SignUpFactory(_signUpPrefab);
        }

        public void Initialization()
        {
            var signUpMenu = _signUpFactory.Create(_mainParent.transform);
            
            var bgVideo = signUpMenu.GetComponentInChildren<VideoPlayer>();
            bgVideo.renderMode = VideoRenderMode.CameraFarPlane;
            bgVideo.targetCamera = Camera.main;
            bgVideo.Play();
            
            signUpMenu.transform.localPosition = new Vector3(0,0,0);

            SignUpButtons = new List<Button>();
            SignUpButtons.AddRange(signUpMenu.GetComponentsInChildren<Button>());

            new SignUpAddButtonsToDictionary(SignUpButtons, _gameContextWithViews);
            var authLogic = new SignUpLogic(_gameContextWithViews.Buttons);
            authLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.LoadSignUp, signUpMenu);
            _gameContextWithUI.AddUILogic(LoadingParts.LoadSignUp, authLogic);
        }
    }
}