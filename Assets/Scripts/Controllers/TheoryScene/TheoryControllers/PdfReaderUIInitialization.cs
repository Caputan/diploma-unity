using System.IO;
using Diploma.Interfaces;
using Diploma.Managers;
using TMPro;
using UI.TheoryUI.PDFReaderUI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class PdfReaderUIInitialization: IInitialization
    {
        private readonly FileManager _fileManager;
        private readonly GameObject _parent;
        private PDFReaderUIFactory _factory;

        public PdfReaderUIInitialization(GameObject prefab, GameObject parent,FileManager fileManager)
        {
            _fileManager = fileManager;
            _parent = parent;
            _factory = new PDFReaderUIFactory(prefab);
        }
        public void Initialization() { }
        public void ReadANewPdfDocument(string mainPath)
        {
            #region Creation PDF Reader

            var paths = Directory.GetFiles(Path.Combine(mainPath,_fileManager.GetStorage()));
            
            foreach(var path in paths)
            {
                //загрузка картинки в текстуру
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                //создание элемента UI
                
                var pdfReaderObject = _factory.Create(_parent.transform.GetChild(2).GetChild(0).GetChild(0).transform);
                //pdfReaderObject.transform.localPosition = new Vector3(0, 0, 0);
                pdfReaderObject.GetComponentInChildren<RawImage>().texture = tex;

            }
            #endregion
        }
    }
}