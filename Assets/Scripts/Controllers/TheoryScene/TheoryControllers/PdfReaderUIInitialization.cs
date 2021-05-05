using System.Collections;
using System.IO;
using Coroutine;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using TMPro;
using Tools;
using UI.TheoryUI.PDFReaderUI;
using UI.TheoryUI.VideoFactory;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class PdfReaderUIInitialization: IInitialization
    {
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly VideoPlayer _videoPlayer;
        private Transform _parent;
        private PDFReaderUIFactory _factory;
        private VideoFactory _videoFactory;

        private readonly ResourcePath _viewPath = new ResourcePath {PathResource = "Prefabs/TheoryScene/ImageForPage"};
        private readonly ResourcePath _viewVideoPrefabPath = 
            new ResourcePath {PathResource = "Prefabs/TheoryScene/VideoPlayer"};
        private readonly ResourcePath _videoTexture =
            new ResourcePath {PathResource = "Prefabs/TheoryScene/Textures/Video"};
        public PdfReaderUIInitialization(GameContextWithViewsTheory gameContextWithViewsTheory,
            VideoPlayer videoPlayer
            )
        {
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _videoPlayer = videoPlayer;
            _factory = new PDFReaderUIFactory(ResourceLoader.LoadPrefab(_viewPath));
            _videoFactory = new VideoFactory(ResourceLoader.LoadPrefab(_viewVideoPrefabPath));
        }
        public void Initialization() { }
        
        public void ReadNextDoc(int id)
        {
            ReadANewPdfDocument(id).StartCoroutine(out _, out _);
        }
        public IEnumerator ReadANewPdfDocument(int id)
        {
            _videoPlayer.Stop();
            yield return new WaitForEndOfFrame();
            
            #region Creation PDF Reader
            _parent = _gameContextWithViewsTheory.Parents[0];
            _parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1000,1300);
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

        public void PlayANewVideo()
        {
            _parent = _gameContextWithViewsTheory.Parents[0];
            _parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1350,750);
            GameObject videoObject = _videoFactory.Create(_parent);
            videoObject.GetComponent<RawImage>().texture = ResourceLoader.LoadObject<Texture>(_videoTexture);
            _videoPlayer.url = _gameContextWithViewsTheory.urlVideo;
            _videoPlayer.Play();
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