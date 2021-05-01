using Interfaces;
using UnityEngine;

namespace UI.PauseUI
{
    public class PauseFactory: IUIObjectsFactory
    {
        private readonly GameObject _signUpPrefab;
        
        public PauseFactory(GameObject signUpPrefab)
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