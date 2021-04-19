using Interfaces;
using UnityEngine;

namespace UI.TheoryUI.PDFReaderUI
{
    public class PDFReaderUIFactory: IUIObjectsFactory
    {
        private readonly GameObject _chooseLessonConstructorPrefab;

        public PDFReaderUIFactory(GameObject chooseLessonConstructorPrefab)
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