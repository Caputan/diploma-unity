using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class AuthFactory: IUIObjectsFactory
    {
        private readonly GameObject _authPrefab;

        public AuthFactory(GameObject authPrefab)
        {
            _authPrefab = authPrefab;
        }
        
        public GameObject Create(Transform Parent)
        {
            GameObject gm = GameObject.Instantiate(_authPrefab, Parent, true);

            return gm;
        }
    }
}