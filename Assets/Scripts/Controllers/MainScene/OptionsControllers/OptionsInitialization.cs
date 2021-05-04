using System.Collections.Generic;
using System.Linq;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
using Tools;
using UI.Options;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class OptionsInitialization: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly GameObject _optionsParent;
        private OptionsFactory _optionsFactory;
        private List<Button> _optionsButtons;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/MainScene/Options_Prefab"};
        
        public OptionsInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            GameObject optionsParent
            )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _optionsParent = optionsParent;
            _optionsFactory = new OptionsFactory(ResourceLoader.LoadPrefab(_viewPath));
        }
        
        public void Initialization()
        {
            #region Options Creation

            var options = _optionsFactory.Create(_optionsParent.transform);
            options.transform.localPosition = new Vector3(0,0,0);
            
            _optionsButtons = new List<Button>();
            _optionsButtons.AddRange(options.GetComponentsInChildren<Button>());

            var sliders = options.GetComponentsInChildren<Slider>().ToList();
            
            new OptionsAddButtonsToDictionary(_optionsButtons,sliders,_gameContextWithViews);

            var optionsLogic = new OptionsLogic(_gameContextWithViews.OptionsButtons,_gameContextWithViews.Sliders);
            optionsLogic.Initialization();
            
            
            _gameContextWithUI.AddUIToDictionary(LoadingParts.Options, options);
            _gameContextWithUI.AddUILogic(LoadingParts.Options,optionsLogic);
            
            #endregion
        }
    }
}