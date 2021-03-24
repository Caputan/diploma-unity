using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class MainMenuFactory: IUIObjectsFactory
    {
        private readonly GameObject _mainMenuPrefab;

        public MainMenuFactory(GameObject mainMenuPrefab)
        {
            _mainMenuPrefab = mainMenuPrefab;
        }
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_mainMenuPrefab, Parent, true);
            
            return gm;
        }
    }
}