using Controllers.TheoryScene.TheoryControllers;
using Data;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Interfaces;
using UnityEngine;

namespace Controllers.TheoryScene.UIController
{
    public class UIControllerTheoryScene: IInitialization, ICleanData
    {
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly GameContextWithUITheory _gameContextWithUITheory;
        private readonly LoadingSceneController _loadingSceneController;
        private readonly TheoryController _theoryController;
        private readonly LibraryTreeController _libraryController;
        private readonly AdditionalInfomationLibrary _additionalInfomationLibrary;
        private readonly GameObject _backGround;
        private ErrorHandler _errorHandler;
        private LoadingPartsTheoryScene _currentPosition;

        public UIControllerTheoryScene(
            GameContextWithViewsTheory gameContextWithViewsTheory,
            GameContextWithUITheory gameContextWithUITheory,
            LoadingSceneController loadingSceneController,
            TheoryController theoryController,
            LibraryTreeController libraryController,
            AdditionalInfomationLibrary additionalInfomationLibrary
        )
        {
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _gameContextWithUITheory = gameContextWithUITheory;
            _loadingSceneController = loadingSceneController;
            _theoryController = theoryController;
            _libraryController = libraryController;
            _additionalInfomationLibrary = additionalInfomationLibrary;

            _backGround = GameObject.Find("BackGround");
            ShowUIByUIType(LoadingPartsTheoryScene.FirstOpen);
        }
        public void Initialization()
        {

            foreach (var value in _gameContextWithUITheory.UILogic)
            {
                Debug.Log(value.Key);
                var i = (ITheorySceneButton)value.Value;
                i.LoadNext += ShowUIByUIType;
            }
            foreach (var value in _gameContextWithUITheory.UITreeLogic)
            {
                Debug.Log(value.Key);
                var i = (ILibraryOnSceneButton)value.Value;
                i.LoadNext += ShowLibraryObject;
            }

            SetVisiableBack(false);
        }

        public void CleanData()
        {
            foreach (var value in _gameContextWithUITheory.UILogic)
            {
                Debug.Log(value.Key);
                var i = (ITheorySceneButton)value.Value;
                i.LoadNext -= ShowUIByUIType;
            }
            foreach (var value in _gameContextWithUITheory.UITreeLogic)
            {
                Debug.Log(value.Key);
                var i = (ILibraryOnSceneButton)value.Value;
                i.LoadNext -= ShowLibraryObject;
            }
        }
        
        private void HideUI(GameObject Controller)
        {
            Controller.SetActive(false);
        }

        public void HideAllUI()
        {
            foreach (var controller in _gameContextWithUITheory.UiControllers)
            {
                HideUI(controller.Value);
            }
        }
        
        
        private void ShowLibraryObject(int id)
        {
            _theoryController.CleanData();
            _libraryController.Initialization(_additionalInfomationLibrary.libraryObjcets[id].file);
            _gameContextWithViewsTheory.TheoryButtons[LoadingPartsTheoryScene.LoadPractise].
                gameObject.SetActive(false);
            
            SetVisiableBack(true);
        }

        private void SetVisiableBack(bool set)
        {
            if (set)
            {
                _gameContextWithViewsTheory.TheoryButtons[LoadingPartsTheoryScene.ExitToMain].
                    gameObject.SetActive(false);
                _gameContextWithViewsTheory.TheoryButtons[LoadingPartsTheoryScene.CloseLibrary].
                    gameObject.SetActive(true);
            }
            else
            {
                _gameContextWithViewsTheory.TheoryButtons[LoadingPartsTheoryScene.ExitToMain].
                    gameObject.SetActive(true);
                _gameContextWithViewsTheory.TheoryButtons[LoadingPartsTheoryScene.CloseLibrary].
                    gameObject.SetActive(false);
            }
        }
        
        public void ShowUIByUIType(LoadingPartsTheoryScene id)
        {
            HideAllUI();
            switch (id)
            {
                case LoadingPartsTheoryScene.FirstOpen:
                    _theoryController.Initialization();
                    Initialization();
                    break;
                case LoadingPartsTheoryScene.ExitToMain:
                    _theoryController.RemoveDocumentPng();
                    Debug.Log("GoingToMenu");
                    _loadingSceneController.LoadNextScene(0);
                    break;
                case LoadingPartsTheoryScene.LoadPractise:
                    Debug.Log("GoingToPractice");
                    _loadingSceneController.LoadNextScene(2);
                    break;
                case LoadingPartsTheoryScene.CloseLibrary:
                    _libraryController.CleanData();
                    _theoryController.Initialization();
                    _gameContextWithViewsTheory.TheoryButtons[LoadingPartsTheoryScene.LoadPractise].
                        gameObject.SetActive(true);
                    SetVisiableBack(false);
                    break;
            }
            Debug.Log(id);
        }
    }
}