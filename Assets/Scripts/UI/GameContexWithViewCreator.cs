using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class GameContexWithViewCreator: IInitialization
    {
        private readonly GameContextWithViews _gameContextWithViews;
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly Transform _canvasParent;
        private GameObject _prefabTogglePanel;
        private TogglePanelFactory _togglePanelFactory;
        private FactoryType _factoryType;
        
        public GameContexWithViewCreator(GameContextWithViews gameContextWithViews,GameContextWithLogic gameContextWithLogic,
            Transform CanvasParent,GameObject PrefabTogglePanel)
        {
            _gameContextWithViews = gameContextWithViews;
            _gameContextWithLogic = gameContextWithLogic;
            _canvasParent = CanvasParent;
            _prefabTogglePanel = PrefabTogglePanel;
            _togglePanelFactory = new TogglePanelFactory(_prefabTogglePanel);
        }

        public void Initialization()
        {
            Transform transformToggleGroup = 
                GameObject.Instantiate(new GameObject()).transform;
            transformToggleGroup.SetParent(_canvasParent);
            
            //костыль. как только будет бд - переписать в норму
            var v1 = _togglePanelFactory.Create(transformToggleGroup);
            var v2 = _togglePanelFactory.Create(transformToggleGroup);
            var v3 = _togglePanelFactory.Create(transformToggleGroup);

            // foreach (var VARIABLE in COLLECTION)
            // {
            //     
            // }

            // if (FactoryType.TryParse("GearBox", out _factoryType))
            // {
            //     
            // }
            //
            // _gameContextWithLogic.AddFactoryType(v1.GetInstanceID(),);
            // _gameContextWithLogic.AddFactoryType(v2.GetInstanceID(),convertFromStringToFactoryType("OppositionEngine"));
            // _gameContextWithLogic.AddFactoryType(v3.GetInstanceID(),convertFromStringToFactoryType("BrakeFactory"));
            //
            // _gameContextWithViews.AddToggles(v1.GetInstanceID(),v1);
            // _gameContextWithViews.AddToggles(v2.GetInstanceID(),v2);
            // _gameContextWithViews.AddToggles(v3.GetInstanceID(),v3);
        }
    }
}