using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class LessonConstructorUIFactory: IUIObjectsFactory
    {
        private readonly GameObject _chooseLessonConstructorPrefab;

        public LessonConstructorUIFactory(GameObject chooseLessonConstructorPrefab)
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