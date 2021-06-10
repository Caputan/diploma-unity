using System.Collections.Generic;
using Controllers;
using Data;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.PracticeScene.GameContext;
using UI.CompleteUI;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.Controllers
{
    public sealed class PracticeSceneController: IInitialization, ICleanData
    {
        // данный класс должен подписаться на нужные ему события
        // для завершения сессии или прочих операций
        // список операций:
        // 1. завершение практики и проставление в бд маркера завершенности
        // 2. перезапуск сцены
        // ...
        private readonly GameContextWithView _gameContextWithView;

        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly ImportantDontDestroyData _data;

        private readonly LoadingSceneController _loadingSceneController;

        private readonly GameObject _completeWindow;
        private readonly Transform _completeWindowParent;

        private GameObject _completeGM;

        public PracticeSceneController(
            DataBaseController dataBaseController, 
            List<IDataBase> tables, 
            ImportantDontDestroyData data,
            LoadingSceneController loadingSceneController,
            GameObject completeWindow, 
            Transform completeWindowParent, 
            GameContextWithView gameContextWithView
            )
        {
            _dataBaseController = dataBaseController;
            _tables = tables;
            _data = data;
            
            _loadingSceneController = loadingSceneController;
            _completeWindow = completeWindow;
            _completeWindowParent = completeWindowParent;
            _gameContextWithView = gameContextWithView;
            
            AssembleController.AssembleController.PracticeCompleted += CompleteLesson;
        }

        private void CompleteLesson()
        {
            _dataBaseController.SetTable(_tables[4]);

            var newParams = new[]
            {
                "",
                "",
                "",
                "",
                "",
                " " + _data.lessonID.ToString()
            };
            _dataBaseController.UpdateRecordById(_data.activatedUserID, newParams);
            
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            PlayerInitialization._isPaused = true;
            
            _completeGM.SetActive(true);
        }
        
        public void Initialization()
        {
            _completeGM = GameObject.Instantiate(_completeWindow, _completeWindowParent);
            _completeGM.transform.localPosition = new Vector3(0,0,0);

            var completeButtons = new List<Button>();
            completeButtons.AddRange(_completeGM.GetComponentsInChildren<Button>());

            new CompleteButtonsAdd(completeButtons, _gameContextWithView);
            
            var completeLogic = new CompleteLogic(_gameContextWithView.CompleteButtons);
            completeLogic.Initialization();

            _completeGM.SetActive(false);
        }

        public void CleanData()
        {
            AssembleController.AssembleController.PracticeCompleted -= CompleteLesson;
        }
    }
}