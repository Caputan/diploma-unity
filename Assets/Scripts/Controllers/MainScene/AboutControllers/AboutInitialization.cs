using System.Collections.Generic;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using TMPro;
using Tools;
using UI.About;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers.AboutControllers
{
    public sealed class AboutInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _aboutParent;
        private readonly AboutFactory _factory;
        private List<Button> _buttons;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/About"};

        public AboutInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, GameObject aboutParent)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _aboutParent = aboutParent;
            _factory = new AboutFactory(ResourceLoader.LoadPrefab(_viewPath));
        }

        public void Initialization()
        {
            var about = _factory.Create(_aboutParent.transform);

            about.transform.localPosition = new Vector3(0,0,0);
            
            _buttons = new List<Button>();
            _buttons.AddRange(about.GetComponentsInChildren<Button>());
            

            new AboutAddButtonsToDictionary(_buttons, _gameContextWithViews);
            var aboutLogic = new AboutLogic(_gameContextWithViews.AboutButtons);
            aboutLogic.Initialization();
            _gameContextWithUI.AddUIToDictionary(LoadingParts.About, about);
            _gameContextWithUI.AddUILogic(LoadingParts.About, aboutLogic);
        }
    }
}