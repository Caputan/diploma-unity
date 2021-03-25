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

        public void SetGraphicsQuality(LoadingParts loadingParts)
        {
            //какая-то херня у них с индексами....
            switch (loadingParts)
            {
                case LoadingParts.LowGraphics:
                    QualitySettings.SetQualityLevel(1,true);
                    break;
                case LoadingParts.MiddleGraphics:
                    QualitySettings.SetQualityLevel(2,true);
                    break;
                case LoadingParts.HighGraphics:
                    QualitySettings.SetQualityLevel(3,true);
                    break;
            }
            
        }

        private void SwitchPersent(float persent)
        {
            _gameContextWithViews.Slider.gameObject.
                GetComponentInChildren<TextMeshProUGUI>().text = Math.Round(persent+AudioConstant)+ "%";
            _audioMixer.SetFloat("MasterVol", persent);

        }

        public void DeactivateButton(LoadingParts id)
        {
            switch (id)
            {
                case LoadingParts.LowGraphics:
                    _gameContextWithViews.OptionsButtons[LoadingParts.LowGraphics].interactable = false;
                    _gameContextWithViews.OptionsButtons[LoadingParts.MiddleGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[LoadingParts.HighGraphics].interactable = true;
                    break;
                case LoadingParts.MiddleGraphics:
                    _gameContextWithViews.OptionsButtons[LoadingParts.LowGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[LoadingParts.MiddleGraphics].interactable = false;
                    _gameContextWithViews.OptionsButtons[LoadingParts.HighGraphics].interactable = true;
                    break;
                case LoadingParts.HighGraphics:
                    _gameContextWithViews.OptionsButtons[LoadingParts.LowGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[LoadingParts.MiddleGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[LoadingParts.HighGraphics].interactable = false;
                    break;
            }
            
        }

        public void CleanData()
        {
            IUISilder sliderController = (IUISilder) _gameContextWithUI.UILogic[LoadingParts.Options];
            sliderController.ChangePersent -= SwitchPersent;
        }
    }
}