using Interfaces;
using UnityEngine;

namespace UI.Library
{
    public class LibraryUIFactory: IUIObjectsFactory
    {
        private readonly GameObject _chooseLessonConstructorPrefab;

        public LibraryUIFactory(GameObject chooseLessonConstructorPrefab)
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