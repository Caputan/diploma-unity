using Interfaces;
using UnityEngine;

namespace UI.TheoryUI.TheoryLibraryTree
{
    public class TheoryLibraryTreeFactory: IUIObjectsFactory
    {
        private readonly GameObject _chooseLessonConstructorPrefab;

        public TheoryLibraryTreeFactory(GameObject chooseLessonConstructorPrefab)
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