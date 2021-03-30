using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class ErrorMenuFactory: IUIObjectsFactory
    {
        private GameObject _errorMenuPrefab;

        public ErrorMenuFactory(GameObject errorMenuPrafab)
        {
            _errorMenuPrefab = errorMenuPrafab;
        }
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_errorMenuPrefab, Parent, true);
            return gm;
        }
    }
}