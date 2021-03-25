using System;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

namespace Controllers
{
    public class OptionsController: IInitialization,ICleanData
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithUI _gameContextWithUI;
        private readonly AudioMixer _audioMixer;
        private const float AudioConstant = 100;

        public OptionsController(
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, 
            AudioMixer audioMixer)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _audioMixer = audioMixer;
        }
        
        public void Initialization()
        {
            IUISilder sliderController = (IUISilder) _gameContextWithUI.UILogic[LoadingParts.Options];
            sliderController.ChangePersent += SwitchPersent;

        }

        private void SwitchPersent(float persent)
        {
            _gameContextWithViews.Slider.gameObject.
                GetComponentInChildren<TextMeshProUGUI>().text = Math.Round(persent+AudioConstant)+ "%";
            _audioMixer.SetFloat("MasterVol", persent);

        }

        public void CleanData()
        {
            IUISilder sliderController = (IUISilder) _gameContextWithUI.UILogic[LoadingParts.Options];
            sliderController.ChangePersent -= SwitchPersent;
        }
    }
}