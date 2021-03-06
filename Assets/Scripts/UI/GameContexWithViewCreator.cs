using System;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class GameContexWithViewCreator: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly GameObject _canvasParent;
        private GameObject _prefabTogglePanel;
        private TogglePanelFactory _togglePanelFactory;
        private FactoryType _factoryType;
        
        public GameContexWithViewCreator(GameContextWithViews gameContextWithViews,GameContextWithLogic gameContextWithLogic,
            GameObject CanvasParent,GameObject PrefabTogglePanel)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            _canvasParent = CanvasParent;
            _prefabTogglePanel = PrefabTogglePanel;
            _togglePanelFactory = new TogglePanelFactory(_prefabTogglePanel,_canvasParent.GetComponent<ToggleGroup>());
        }

        public void Initialization()
        {
            foreach (FactoryType factoryType in Enum.GetValues(typeof(FactoryType)))
            {
                var toggle = _togglePanelFactory.Create(_canvasParent.transform);
                _gameContextWithLogic.AddFactoryTypeForCreating(toggle.GetInstanceID(),factoryType);
                _gameContextWithViews.AddToggles(toggle.GetInstanceID(),toggle);
            }
        }
    }
}