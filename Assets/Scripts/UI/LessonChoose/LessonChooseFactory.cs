using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class LessonChooseFactory: IUIObjectsFactory
    {
        private readonly GameObject _chooseLessonPrefab;

        public LessonChooseFactory(GameObject chooseLessonPrefab)
        {
            _chooseLessonPrefab = chooseLessonPrefab;
        }
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_chooseLessonPrefab, Parent, true);
            
            return gm;
        }
    }
}