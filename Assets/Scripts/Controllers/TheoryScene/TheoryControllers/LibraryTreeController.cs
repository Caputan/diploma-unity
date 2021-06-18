using System;
using System.Collections;
using System.Collections.Generic;
using Coroutine;
using Data;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using PDFWorker;
using UI.LoadingUI;
using UnityEngine;
using Types = Diploma.Tables.Types;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class LibraryTreeController
    {
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly LoadingUILogic _loadingUILogic;
        private readonly AdditionalInfomationLibrary _additionalInformationLibrary;
        private readonly Types _types;
        private PDFReader _pdfReader;
        private Dictionary<int, string> _libraryObjects;
        
        public event Action<Dictionary<int, string>> rewriteDictionary;
        public LibraryTreeController(PdfReaderUIInitialization pdfReaderUIInitialization,
            FileManager fileManager,string pdfStoragePath,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            LoadingUILogic loadingUILogic,
            AdditionalInfomationLibrary additionalInformationLibrary,
            Types types 
        )
        {
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _loadingUILogic = loadingUILogic;
            _additionalInformationLibrary = additionalInformationLibrary;
            _types = types;
            _pdfReader = new PDFReader(fileManager,pdfStoragePath,
                _gameContextWithViewsTheory,
                _loadingUILogic);
        }
        public void Initialization()
        {
            _libraryObjects = new Dictionary<int, string>();
            foreach (var libraryObject in _additionalInformationLibrary.libraryObjcets)
            foreach (var type in _types.TypeS.Split(','))
            {
                if(type!="")
                    if (libraryObject.id == Convert.ToInt32(type))
                    {
                        _libraryObjects.Add(libraryObject.id, libraryObject.file);
                    }
            }
            rewriteDictionary?.Invoke(_libraryObjects);
            //CreateDocumentLocaly(_libraryObjects);
        }

        public void Show(int id)
        {
            Debug.Log(id);
            if (id == -2)
            {
                _pdfReaderUIInitialization.UnloadDocument();
                _pdfReaderUIInitialization.PlayANewVideo();
            }
            else
            {
                _pdfReaderUIInitialization.UnloadVideo();
                _pdfReaderUIInitialization.ReadNextDoc(id);
            }
        }

        public void CreateDocumentLocaly( 
            int id,
            string obj,
            List<CoroutineController> coroutineController,
            int hand
            )
        {
            _pdfReader.RaedFile(id,obj,coroutineController,hand);
        }

        public void RemoveDocumentPng()
        {
            _pdfReader.DeleteStorage();
        }
    }
}