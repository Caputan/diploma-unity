using Interfaces;
using UnityEngine;

namespace UI.TheoryUI
{
    public class TheoryUIFactory: IUIObjectsFactory
    {
        private readonly GameObject _chooseLessonConstructorPrefab;

        public TheoryUIFactory(GameObject chooseLessonConstructorPrefab)
        {
            _chooseLessonConstructorPrefab = chooseLessonConstructorPrefab;
        }
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_chooseLessonConstructorPrefab, Parent, true);
            
            return gm;
        }
        
    }
}