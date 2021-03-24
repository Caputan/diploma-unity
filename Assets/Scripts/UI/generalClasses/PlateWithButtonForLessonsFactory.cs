using Interfaces;
using UnityEngine;

namespace Diploma.UI
{
    public class PlateWithButtonForLessonsFactory: IUIObjectsFactory
    {
        private readonly GameObject _prefabLessonView;

        public PlateWithButtonForLessonsFactory(GameObject prefabLessonView)
        {
            _prefabLessonView = prefabLessonView;
        }
        
        public GameObject Create(Transform Parent)
        {
            var gm = GameObject.Instantiate(_prefabLessonView, Parent, true);
            return gm;
        }
    }
}