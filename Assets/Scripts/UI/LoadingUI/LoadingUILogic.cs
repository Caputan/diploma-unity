using System.Collections;
using Coroutine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LoadingUI
{
    public sealed class LoadingUILogic
    {
        private readonly GameObject _settingActiveGameObject;
        private readonly TextMeshProUGUI _textMeshProUGUI;
        private readonly TextMeshProUGUI _textMeshProUGUIWhatIsLoading;
        private readonly Slider _slider;
        private readonly Transform _canvas;


        public LoadingUILogic(
            GameObject settingActiveGameObject,
            TextMeshProUGUI textMeshProUGUI,
            TextMeshProUGUI textMeshProUGUIWhatIsLoading,
            Slider slider,
            Transform canvas
            )
        {
            _settingActiveGameObject = settingActiveGameObject;
            _textMeshProUGUI = textMeshProUGUI;
            _textMeshProUGUIWhatIsLoading = textMeshProUGUIWhatIsLoading;
            _slider = slider;
            _canvas = canvas;
        }
        
        public void SetActiveLoading(bool active)
        {
            _settingActiveGameObject.SetActive(active);
            _settingActiveGameObject.transform.SetParent(_canvas);
            _settingActiveGameObject.transform.localPosition = new Vector3(0,0,0);

        }

        // public void SetLoadingParameter(float parameterForSlider,float parameterFoText)
        // {
        //     LoadingParams(parameterForSlider,parameterFoText).StartCoroutine(out _);
        // }

        //public IEnumerator LoadingParams(float parameterForSlider,float parameterFoText, string whatIsLoading)
        public void LoadingParams(float parameterForSlider,float parameterFoText, string whatIsLoading)
        {
            _textMeshProUGUIWhatIsLoading.text = "Загружается: " + whatIsLoading;
            _textMeshProUGUI.text = "Загружено " + parameterFoText +"%";
            _slider.value = parameterForSlider;
            //yield return null;
        }
        
    }
}