using System.Collections;
using Coroutine;
using Diploma.Interfaces;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace UI.LoadingUI
{
    public sealed class LoadingUILogic: IInitialization
    {
        
        private readonly Transform _canvas;
        public GameObject _settingActiveGameObject;
        private TextMeshProUGUI _textMeshProUGUI;
        private TextMeshProUGUI _textMeshProUGUIWhatIsLoading;
        private Slider _slider;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/TheoryScene/Loading"};
        public LoadingUILogic(
            Transform canvas
            )
        {
            _canvas = canvas;
        }
        
        public void SetActiveLoading(bool active)
        {
            _settingActiveGameObject.SetActive(active);
            _settingActiveGameObject.transform.localPosition = new Vector3(0,0,0);
        }

        public void LoadingParams(float parameterForSlider,float parameterFoText, string whatIsLoading)
        {
            
            _textMeshProUGUIWhatIsLoading.text = "Загружается: " + whatIsLoading;
            _textMeshProUGUI.text = "Загружено " + parameterFoText +"%";
            _slider.value = parameterForSlider;
        }
        
        public void LoadingParams(string parameterFoText, string whatIsLoading)
        {
            
            _textMeshProUGUIWhatIsLoading.text = whatIsLoading;
            _textMeshProUGUI.text = parameterFoText;
            _slider.value = 0;
        }

        public void Initialization()
        {
            _settingActiveGameObject =Object.Instantiate(
                ResourceLoader.LoadPrefab(_viewPath),
                _canvas, 
                true);
            _textMeshProUGUI = _settingActiveGameObject.GetComponentsInChildren<TextMeshProUGUI>()[0];
            _textMeshProUGUIWhatIsLoading = _settingActiveGameObject.GetComponentsInChildren<TextMeshProUGUI>()[1];
            _slider = _settingActiveGameObject.GetComponentInChildren<Slider>();
        }
    }
}