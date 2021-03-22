using System;
using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using UnityEngine;
using UnityEngine.UI;
using Types = Diploma.Tables.Types;


namespace Diploma.UI
{
    public class LessonConstructorUIAddButtonsToDictionary: IInitialization
    {
        private readonly List<Button> _buttons;
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;
        private readonly GameObject _prefabTogglePanel;
        private readonly GameObject _canvasParent;
        private readonly int[] _usedMenus = new[] {10,9,12,13,14};
        private TogglePanelFactory _togglePanelFactory;
        
        public LessonConstructorUIAddButtonsToDictionary(
            List<Button> buttons, 
            GameContextWithViews gameContextWithViews,GameContextWithLogic gameContextWithLogic
            ,DataBaseController dataBaseController, List<IDataBase> tables
            ,GameObject prefabTogglePanel,GameObject canvasParent
        )
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            _dataBaseController = dataBaseController;
            _tables = tables;
            _prefabTogglePanel = prefabTogglePanel;
            _canvasParent = canvasParent;
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddLessonConstructorButtons((LoadingParts)_usedMenus[i],button);
                i++;
            }
            _togglePanelFactory = new TogglePanelFactory(_prefabTogglePanel,_canvasParent.GetComponent<ToggleGroup>());
        }

        public void Initialization()
        {
            #region Creation Toggles Creation
            
            _dataBaseController.SetTable(_tables[3]);
            List<Types> dataTypesFromTable = _dataBaseController.GetDataFromTable<Types>();
            int index = 0;
            foreach (FactoryType factoryType in Enum.GetValues(typeof(FactoryType)))
            {
                var toggle = _togglePanelFactory.Create(_canvasParent.transform);
                toggle.transform.localPosition = new Vector3(0, 0, 0);
                //var tex = new Texture2D(5, 5);
                //tex.LoadImage(File.ReadAllBytes(dataTypesFromTable[index].Type_Image));
                //toggle.GetComponentInChildren<RawImage>().texture = tex;
                _gameContextWithLogic.AddFactoryTypeForCreating(toggle.GetInstanceID(), factoryType);
                _gameContextWithViews.AddToggles(toggle.GetInstanceID(), toggle);
                index++;
            }
            
            #endregion
        }
    }
}