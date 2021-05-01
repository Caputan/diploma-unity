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
        private const float AudioMixerLowConstant = -80;


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

        public void SetGraphicsQuality(OptionsButtons loadingParts)
        {
            //какая-то херня у них с индексами....
            switch (loadingParts)
            {
                case OptionsButtons.LowGraphics:
                    QualitySettings.SetQualityLevel(1,true);
                    break;
                case OptionsButtons.MiddleGraphics:
                    QualitySettings.SetQualityLevel(2,true);
                    break;
                case OptionsButtons.HighGraphics:
                    QualitySettings.SetQualityLevel(3,true);
                    break;
            }
            
        }

        private void SwitchPersent(float persent)
        {
            _gameContextWithViews.Slider.gameObject.
                
                // от 0 до 100
                // от -80 до 0
                
                // 0 - 100
                // -80 - 0
                
                
                GetComponentInChildren<TextMeshProUGUI>().text =
                Math.Round(AudioConstant-(persent/(AudioMixerLowConstant)*AudioConstant))+ "%";
            _audioMixer.SetFloat("MasterVol", persent);

        }

        public void DeactivateButton(OptionsButtons id)
        {
            switch (id)
            {
                case OptionsButtons.LowGraphics:
                    _gameContextWithViews.OptionsButtons[OptionsButtons.LowGraphics].interactable = false;
                    _gameContextWithViews.OptionsButtons[OptionsButtons.MiddleGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[OptionsButtons.HighGraphics].interactable = true;
                    break;
                case OptionsButtons.MiddleGraphics:
                    _gameContextWithViews.OptionsButtons[OptionsButtons.LowGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[OptionsButtons.MiddleGraphics].interactable = false;
                    _gameContextWithViews.OptionsButtons[OptionsButtons.HighGraphics].interactable = true;
                    break;
                case OptionsButtons.HighGraphics:
                    _gameContextWithViews.OptionsButtons[OptionsButtons.LowGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[OptionsButtons.MiddleGraphics].interactable = true;
                    _gameContextWithViews.OptionsButtons[OptionsButtons.HighGraphics].interactable = false;
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