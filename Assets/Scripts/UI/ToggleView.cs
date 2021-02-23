using Diploma.Controllers;
using Diploma.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Diploma.UI
{
    public class ToggleView: IInitialization
    {
        private readonly GameContext _gameContext;

        public ToggleView(GameContext gameContext)
        {
            _gameContext = gameContext;
        }
        public void Initialization()
        {
            foreach (var toggle in _gameContext.ChoosenToggles)
            {
                toggle.onValueChanged.AddListener(on=>
                {
                    if (on) Handle(toggle);
                });
            }
        }

        private void Handle(Toggle toggleValue)
        {
            Debug.Log(toggleValue.name);
        }
    }
}