using System.Collections.Generic;
using System.Linq;
using GameObjectCreating;
using Managers;
using UI.TheoryUI.PDFReaderUI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class Textures2DPool
    {
        private readonly PDFReaderUIFactory _factory;
        private readonly Dictionary<int,RawImage> _gameObjectPool;
       
        private int _count;
        public Transform _rootPool;
        public Textures2DPool(Transform parent, PDFReaderUIFactory factory)
        {
            _factory = factory;
            _gameObjectPool = new Dictionary<int, RawImage>();
            _count = 0;
            
            if (!_rootPool)
            {
                _rootPool = parent.transform;
            }
        }

        public Transform GetPool()
        {
            return _rootPool;
        }
        
        public RawImage GetTexture2D(int idOfElement)
        {
            return _gameObjectPool[idOfElement];
        }

        public int AskForFreeElement()
        {
            if (_gameObjectPool.Count != 0)
            {
                foreach (var rawImage in _gameObjectPool)
                {
                    if (rawImage.Value.texture == null)
                    {
                        return rawImage.Key;
                    }
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }
        public RawImage AddInfoInPool(int id)
        {
            if (id == -1)
            {
                GameObject pdfReaderObject = _factory.Create(_rootPool);
                _gameObjectPool.Add(pdfReaderObject.GetInstanceID(),pdfReaderObject.GetComponent<RawImage>());
                return _gameObjectPool.Last().Value;
            }
            else
            {
                return _gameObjectPool[id];
            }
        }

        public void TurnOffAll()
        {
            foreach (var rawImage in _gameObjectPool)
            {
                rawImage.Value.gameObject.SetActive(false);
            }
        }

        public void TurnOnAll()
        {
            foreach (var rawImage in _gameObjectPool)
            {
                rawImage.Value.gameObject.SetActive(true);
            }
        }
        
        public void TurnOffFree()
        {
            foreach (var rawImage in _gameObjectPool)
            {
                if (rawImage.Value.texture == null)
                {
                    rawImage.Value.gameObject.SetActive(false);
                }
            }
        }
        
        public void SetAllTexturesFree()
        {
            foreach (var rawImage in _gameObjectPool)
            {
                rawImage.Value.texture = null;
            }
        }
    }
}