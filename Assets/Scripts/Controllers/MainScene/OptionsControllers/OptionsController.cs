using System;
using Data;
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
        private readonly ImportantDontDestroyData _importantDontDestroyData;
        private const float AudioConstant = 100;
        private const float AudioMixerLowConstant = -80;


        public OptionsController(
            GameContextWithViews gameContextWithViews,
            GameContextWithUI gameContextWithUI, 
            AudioMixer audioMixer,
            ImportantDontDestroyData importantDontDestroyData)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithUI = gameContextWithUI;
            _audioMixer = audioMixer;
            _importantDontDestroyData = importantDontDestroyData;
            if (_importantDontDestroyData.mouseSensitivity <= 0 
                || _importantDontDestroyData.mouseSensitivity >= 1.001f)
            {
                _importantDontDestroyData.mouseSensitivity = 0.289f;
            }
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

        private void SwitchPersent(OptionsButtons optionsButtons,float persent)
        {
            switch (optionsButtons)
            {
                case OptionsButtons.SliderSound:
                    _gameContextWithViews.Sliders[OptionsButtons.SliderSound].gameObject.
                            GetComponentInChildren<TextMeshProUGUI>().text =
                        Math.Round(AudioConstant-(persent/(AudioMixerLowConstant)*AudioConstant))+ "%";
                    _audioMixer.SetFloat("MasterVol", persent);
                    break;
                case OptionsButtons.SliderMouse:
                    // sens [0.01-1]
                    _gameContextWithViews.Sliders[OptionsButtons.SliderMouse].gameObject.
                            GetComponentInChildren<TextMeshProUGUI>().text = Math.Round(persent,3).ToString();
                    _importantDontDestroyData.mouseSensitivity = persent;
                    break;
            }
                
            


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