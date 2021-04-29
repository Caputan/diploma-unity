using System.Collections;
using System.IO;
using Coroutine;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using TMPro;
using Tools;
using UI.TheoryUI.PDFReaderUI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class PdfReaderUIInitialization: IInitialization
    {
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private Transform _parent;
        private PDFReaderUIFactory _factory;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/TheoryScene/ImageForPage"};

        public PdfReaderUIInitialization(GameContextWithViewsTheory gameContextWithViewsTheory)
        {
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _factory = new PDFReaderUIFactory(ResourceLoader.LoadPrefab(_viewPath));
        }
        public void Initialization() { }
        
        public void ReadNextDoc(int id)
        {
            ReadANewPdfDocument(id).StartCoroutine(out _, out _);
        }
        public IEnumerator ReadANewPdfDocument(int id)
        {
            yield return new WaitForEndOfFrame();
            
            #region Creation PDF Reader
            _parent = _gameContextWithViewsTheory.Parents[0];
            var paths = Directory.GetFiles(_gameContextWithViewsTheory.nameOfFolders[id]);
            
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

            yield return null;
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