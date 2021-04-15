using System.IO;
using Diploma.Interfaces;
using TMPro;
using UI.TheoryUI.PDFReaderUI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class PdfReaderUIInitialization: IInitialization
    {
        private readonly Transform _parent;
        private PDFReaderUIFactory _factory;

        public PdfReaderUIInitialization(GameObject prefab, Transform parent)
        {
            _parent = parent;
            _factory = new PDFReaderUIFactory(prefab);
        }
        public void Initialization() { }
        public void ReadANewPdfDocument(string mainPath)
        {
            #region Creation PDF Reader

            var paths = Directory.GetFiles(mainPath);
            
            foreach(var path in paths)
            {
                //загрузка картинки в текстуру
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                //создание элемента UI
                var pdfReaderObject = _factory.Create(_parent);
                //pdfReaderObject.transform.localPosition = new Vector3(0, 0, 0);
                pdfReaderObject.GetComponentInChildren<RawImage>().texture = tex;

            }
            #endregion
        }
    }
}