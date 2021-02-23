using System;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Interfaces;
using UnityEngine.UI;

namespace Diploma.Constructor
{
    public class AbstractView: IAbstractView, IInitialization
    {
        private readonly GameContext _gameContext;
        private Button _button;
        public void ChoosedNextStage()
        {
            throw new NotImplementedException();
        }

        public AbstractView(GameContext gameContext)
        {
            _gameContext = gameContext;
        }
        
        public event Action<FactoryType> NextStage;
        public void Initialization()
        {
            
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => ChoosedNextStage());
        }

        private void CheckTheChoose()
        {
            
        }
    }
}