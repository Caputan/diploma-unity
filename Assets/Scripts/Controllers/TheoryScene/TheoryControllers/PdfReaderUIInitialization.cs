using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private Textures2DPool _textures2DPool;
        private Transform _POOL;
        
        
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

        public void Initialization()
        {
            _parent = _gameContextWithViewsTheory.Parents[0];
            _textures2DPool = new Textures2DPool(_parent,_factory);
            _parent.GetComponent<GridLayoutGroup>().cellSize = new Vector2(1000,1300);
        }
        
        public void ReadNextDoc(int id)
        {
            ReadANewPdfDocument(id).StartCoroutine(out _, out _);
        }
        public IEnumerator ReadANewPdfDocument(int id)
        {
            _videoPlayer.Stop();
            yield return new WaitForEndOfFrame();
            
            #region Creation PDF Reader
            var paths = Directory.GetFiles(_gameContextWithViewsTheory.nameOfFolders[id]);
            _textures2DPool.TurnOnAll();
            _textures2DPool.SetAllTexturesFree();
            IEnumerable<string> query = paths.OrderBy(file => file.Length);
            foreach(var path in query)
            {
                byte[] bytes = File.ReadAllBytes(path);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(bytes);
                var idOfElement = _textures2DPool.AskForFreeElement();
                var needToSetAnTexture = _textures2DPool.AddInfoInPool(idOfElement);
                needToSetAnTexture.texture = tex;
            }
            #endregion
            _textures2DPool.TurnOffFree();
            yield return null;
        }

        public void PlayANewVideo()
        {
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