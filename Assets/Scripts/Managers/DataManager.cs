using System.Data.Linq;
using Diploma.Controllers;
using UnityEngine;

namespace Managers
{
    public class DataManager
    {
        private DataToTransfer _dataToTransfer;

        private GameContextWithLessons _gameContextWithLessons;
        private GameContextWithLogic _gameContextWithLogic;
        private GameContextWithUI _gameContextWithUI;
        private GameContextWithViews _gameContextWithViews;

        public DataManager(GameContextWithLessons gameContextWithLessons, GameContextWithLogic gameContextWithLogic,
            GameContextWithUI gameContextWithUI, GameContextWithViews gameContextWithViews)
        {
            _gameContextWithLessons = gameContextWithLessons;
            _gameContextWithLogic = gameContextWithLogic;
            _gameContextWithUI = gameContextWithUI;
            _gameContextWithViews = gameContextWithViews;
        }

        public void SetDataToTransfer()
        {
            _dataToTransfer = ScriptableObject.CreateInstance<DataToTransfer>();
            
            _dataToTransfer.GameContextWithLessons = _gameContextWithLessons;
            _dataToTransfer.GameContextWithLogic = _gameContextWithLogic;
            _dataToTransfer.GameContextWithUI = _gameContextWithUI;
            _dataToTransfer.GameContextWithViews = _gameContextWithViews;
        }

        public void DeleteDataFromTransfer()
        {
            ScriptableObject.Destroy(_dataToTransfer);
        }
    }
}