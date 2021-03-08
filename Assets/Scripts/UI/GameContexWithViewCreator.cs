using System;
using System.Collections.Generic;
using Controllers;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Types = Diploma.Tables.Types;

namespace Diploma.UI
{
    public class GameContexWithViewCreator: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly GameObject _canvasParent;
        private GameObject _prefabTogglePanel;
        private readonly DataBaseController _dataBaseController;
        private TogglePanelFactory _togglePanelFactory;
        private FactoryType _factoryType;
        
        public GameContexWithViewCreator(GameContextWithViews gameContextWithViews,GameContextWithLogic gameContextWithLogic,
            GameObject CanvasParent,GameObject PrefabTogglePanel,
            DataBaseController dataBaseController,List<IDataBase> tables)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            _canvasParent = CanvasParent;
            _prefabTogglePanel = PrefabTogglePanel;
            _dataBaseController = dataBaseController;
            _togglePanelFactory = new TogglePanelFactory(_prefabTogglePanel,_canvasParent.GetComponent<ToggleGroup>());
            _dataBaseController.SetTable(tables[3]);
        }

        public void Initialization()
        {
            List<Types> dataFromTable = _dataBaseController.GetDataFromTable<Types>();
            
            
            foreach (FactoryType factoryType in Enum.GetValues(typeof(FactoryType)))
            {
                var toggle = _togglePanelFactory.Create(_canvasParent.transform);
                toggle.transform.localPosition = new Vector3(0,0,0);
                //toggle.GetComponentInChildren<Image>().sprite = dataFromTable[4].Type_Image;
                _gameContextWithLogic.AddFactoryTypeForCreating(toggle.GetInstanceID(),factoryType);
                _gameContextWithViews.AddToggles(toggle.GetInstanceID(),toggle);
               
            }
        }
    }
}