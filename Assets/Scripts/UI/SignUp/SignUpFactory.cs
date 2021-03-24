using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class SignUpFactory: IUIObjectsFactory
    {
        private readonly GameObject _signUpPrefab;
        
        public SignUpFactory(GameObject signUpPrefab)
        {
            _signUpPrefab = signUpPrefab;
        }
        
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_signUpPrefab, Parent, true);

            return gm;
        }
    }
}