using Interfaces;
using UnityEngine;

namespace UI.Options
{
    public class OptionsFactory: IUIObjectsFactory
    {
        private readonly GameObject _optionsPrefab;

        public OptionsFactory(GameObject mainMenuPrefab)
        {
            _optionsPrefab = mainMenuPrefab;
        }
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_optionsPrefab, Parent, true);
            
            return gm;
        }
    }
}