using System;
using System.Collections.Generic;
using Data;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
using PDFWorker;
using UI.LoadingUI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class LibraryTreeController: ICleanData
    {
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly AdditionalInfomationLibrary _additionalInfomationLibrary;
        private readonly Types _types;
        private PDFReader _pdfReader;
        private Dictionary<int, string> _libraryObjects;
        
        public LibraryTreeController(PdfReaderUIInitialization pdfReaderUIInitialization,
            FileManager fileManager,string pdfStoragePath,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            LoadingUILogic loadingUILogic,
            AdditionalInfomationLibrary additionalInfomationLibrary,
            Types types
            )
        {
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _additionalInfomationLibrary = additionalInfomationLibrary;
            _types = types;
            _pdfReader = new PDFReader(fileManager,pdfStoragePath,
                _gameContextWithViewsTheory,_pdfReaderUIInitialization,
                loadingUILogic);
        }
        public void Initialization()
        {
            foreach (var libraryObject in _additionalInfomationLibrary.libraryObjcets)
            foreach (var type in _types.TypeS.Split(','))
            {
                if (libraryObject.id == Convert.ToInt32(type))
                {
                    if(type!="")
                        _libraryObjects.Add(libraryObject.id, libraryObject.file);
                }
            }
            CreateDocumentLocaly(_libraryObjects);
        }

        public void Show(int id)
        {
            _pdfReaderUIInitialization.ReadNextDoc(id);
        }
        
        public void CleanData()
        {
            UnloadDocument();
        }
        
        private void CreateDocumentLocaly( Dictionary<int, string> libraryObjects)
        {
            _pdfReader.RaedFile(libraryObjects);
        }

        public void RemoveDocumentPng()
        {
            _pdfReader.DeleteStorage();
        }

        

        private void UnloadDocument()
        {
            _pdfReaderUIInitialization.UnloadDocument();
        }
    }
}