using Interfaces;
using UnityEngine;

namespace UI.About
{
    public sealed class AboutFactory: IUIObjectsFactory
    {
        private readonly GameObject _aboutPrefab;

        public AboutFactory(GameObject aboutPrefab)
        {
            _aboutPrefab = aboutPrefab;
        }
        
        public GameObject Create(Transform Parent)
        {
            GameObject gm = GameObject.Instantiate(_aboutPrefab, Parent, true);
           
            return gm;
        }
    }
}