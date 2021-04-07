using Interfaces;
using UnityEngine;

namespace UI.AnimationCreator
{
    public sealed class AnimatorFactory: IUIObjectsFactory
    {
        private readonly GameObject _animatorPrefab;

        public AnimatorFactory(GameObject authPrefab)
        {
            _animatorPrefab = authPrefab;
        }
        
        public GameObject Create(Transform Parent)
        {
            GameObject gm = GameObject.Instantiate(_animatorPrefab, Parent, true);
           
            return gm;
        }
        
       
    }
}