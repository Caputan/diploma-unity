using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.UI;
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
        private readonly GameObject _optionsPrefab;
        private OptionsFactory _optionsFactory;
        private List<Button> OptionsButtons;

        public OptionsInitialization(GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI,
            GameObject optionsParent,
            GameObject optionsPrefab
            )
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _optionsParent = optionsParent;
            _optionsPrefab = optionsPrefab;
            _optionsFactory = new OptionsFactory(_optionsPrefab);
        }
        
        public void Initialization()
        {
            #region Options Creation

            var Options = _optionsFactory.Create(_optionsParent.transform);
            Options.transform.localPosition = new Vector3(0,0,0);
            
            OptionsButtons = new List<Button>();
            OptionsButtons.AddRange(Options.GetComponentsInChildren<Button>());

            var Slider = Options.GetComponentInChildren<Slider>();
            
            new OptionsAddButtonsToDictionary(OptionsButtons,_gameContextWithViews);
            
            _gameContextWithViews.SetSlider(Slider);

            var OptionsLogic = new OptionsLogic(_gameContextWithViews.OptionsButtons,_gameContextWithViews.Slider);
            OptionsLogic.Initialization();
            
            
            _gameContextWithUI.AddUIToDictionary(LoadingParts.Options, Options);
            _gameContextWithUI.AddUILogic(LoadingParts.Options,OptionsLogic);
            
            #endregion
        }
    }
}