using System.IO;
using Diploma.Controllers;
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
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private Transform _parent;
        private PDFReaderUIFactory _factory;

        public PdfReaderUIInitialization(GameObject prefab, FileManager fileManager,
            GameContextWithViewsTheory gameContextWithViewsTheory)
        {
            _fileManager = fileManager;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;

            _factory = new PDFReaderUIFactory(prefab);
        }
        public void Initialization() { }
        public void ReadANewPdfDocument()
        {
            #region Creation PDF Reader
            _parent = _gameContextWithViewsTheory.Parents[0];
            var paths = Directory.GetFiles(_gameContextWithViewsTheory.nameOfFolder);
            
            foreach(var path in paths)
            {
                //загрузка картинки в текстуру
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                //создание элемента UI
                
                GameObject pdfReaderObject = _factory.Create(_parent);
                //pdfReaderObject.transform.localPosition = new Vector3(0, 0, 0);
                pdfReaderObject.GetComponentInChildren<RawImage>().texture = tex;

            }
            #endregion
        }

        public void UnloadDocument()
        {
            for(int i = 0;i<_parent.childCount; i++)
            {
                Object.Destroy(_parent.GetChild(i).gameObject);
            }
            
        }
    }
}