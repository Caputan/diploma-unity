using System;
using Data;
using DG.Tweening;
using Diploma.Controllers;
using Diploma.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace DoTween
{
    public sealed class MessageBoxBehaviour : MonoBehaviour
    {
        [SerializeField] private Button _buttonHide;

        [SerializeField] private ScaleData _pointerUpScale;
        [SerializeField] private GameObject _root;
        [SerializeField] private Transform _message;
        [SerializeField] private Image _background;
        [SerializeField] private float _duration = 0.5f;
        public UIController UIController;
        
        private void Start()
        {
            Hide(0);
        }

        private void OnEnable()
        {
            _buttonHide.onClick.AddListener(ButtonHide_OnClick);
        }
        
        private void OnDisable()
        {
            _buttonHide.onClick.AddListener(ButtonHide_OnClick);
        }

        public void Show()
        {
            _root.SetActive(true);
            // _buttonHide.transform.DOKill();
            // _buttonHide.transform.DOScale(new Vector3(_pointerUpScale.Scale.x, _pointerUpScale.Scale.y, 
            //     _pointerUpScale.Scale.x), _pointerUpScale.Duration).SetEase(_pointerUpScale.Ease);
            // Sequence sequence = DOTween.Sequence();
            // sequence.Insert(0.0f, _background.DOFade(0.5f, _duration));
            // sequence.Insert(0.0f, _message.DOScale(Vector3.one, _duration));
            // sequence.OnComplete(() =>
            // {
            //     sequence = null;
            // });
            
        }

        public void Hide(float duration)
        {
            // Sequence sequence = DOTween.Sequence();
            // sequence.Append(_message.DOScale(Vector3.zero, duration));
            // sequence.Append(_background.DOFade(0.0f, duration));
            // sequence.OnComplete(() =>
            // {
            //     sequence = null;
                _root.SetActive(false);
            //});
        }

        public void ButtonHide_OnClick()
        {
            Debug.Log("Going to Hide");
            UIController.ShowUIByUIType(LoadingParts.Back);
            Hide(_duration);
        }

        private void OnDestroy()
        {
            
        }
    }
}