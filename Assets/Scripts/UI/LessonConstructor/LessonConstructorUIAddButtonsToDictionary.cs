using System;
using System.Collections.Generic;
using Data;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using Diploma.Tables;
using TMPro;
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
        private readonly GameObject _prefabTogglePanel;
        private readonly GameObject _canvasParent;
        private readonly AdditionalInfomationLibrary _libraryDB;
        private readonly int[] _usedMenus = new[] {10,9,12,13,14};
        private TogglePanelFactory _togglePanelFactory;
        
        public LessonConstructorUIAddButtonsToDictionary(
            List<Button> buttons, 
            GameContextWithViews gameContextWithViews,GameContextWithLogic gameContextWithLogic
            ,GameObject prefabTogglePanel,GameObject canvasParent,AdditionalInfomationLibrary libraryDB
        )
        {
            _buttons = buttons;
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            
            _prefabTogglePanel = prefabTogglePanel;
            _canvasParent = canvasParent;
            _libraryDB = libraryDB;
            
            int i = 0;
            foreach (var button in _buttons)
            {
                _gameContextWithViews.AddLessonConstructorButtons((LoadingParts)_usedMenus[i],button);
                i++;
            }
            _togglePanelFactory = new TogglePanelFactory(_prefabTogglePanel);
        }

        public void Initialization()
        {
            #region Creation Toggles Creation

            foreach(var libraryTom in _libraryDB.libraryObjcets)
            {
                var toggle = _togglePanelFactory.Create(_canvasParent.transform);
                toggle.transform.localPosition = new Vector3(0, 0, 0);
                toggle.GetComponentInChildren<RawImage>().texture = libraryTom.texture2D;
                toggle.GetComponentInChildren<TextMeshProUGUI>().text = libraryTom.name;
                _gameContextWithLogic.AddFactoryTypeForCreating(toggle.GetInstanceID(), libraryTom.id);
                _gameContextWithViews.AddToggles(toggle.GetInstanceID(), toggle);
            }
            #endregion
        }
    }
}