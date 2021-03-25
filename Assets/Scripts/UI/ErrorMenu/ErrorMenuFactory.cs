using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class ErrorMenuFactory: IUIObjectsFactory
    {
        private GameObject _errorMenuPrafab;

        public ErrorMenuFactory(GameObject errorMenuPrafab)
        {
            _errorMenuPrafab = errorMenuPrafab;
        }
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_errorMenuPrafab, Parent, true);
            return gm;
        }
    }
}