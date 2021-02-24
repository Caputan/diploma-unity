using System;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine.UI;

namespace Diploma.Constructor
{
    public class AbstractView: IAbstractView, IInitialization
    {
        private readonly GameContextWithLogic _gameContextWithLogic;
        private readonly GameContextWithViews _gameContextWithViews;
        private Button _button;
        private int _toggleID;

        public AbstractView(GameContextWithViews gameContextWithViews, GameContextWithLogic gameContextWithLogic)
        {
            _gameContextWithLogic = gameContextWithLogic;
            _gameContextWithViews = gameContextWithViews;
        }
        
        public event Action<FactoryType> NextStage;
        public void Initialization()
        {
            foreach (var toggle in _gameContextWithViews.ChoosenToggles)
            {
                toggle.Value.GetComponent<Toggle>().onValueChanged.AddListener(on=>
                {
                    if (on) _toggleID = toggle.Value.GetInstanceID();
                });
            }
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ChoosedNextStage());
        }
        
        public void ChoosedNextStage()
        {
            NextStage.Invoke(_gameContextWithLogic.FactoryTypes[_toggleID]);
        }
    }
}