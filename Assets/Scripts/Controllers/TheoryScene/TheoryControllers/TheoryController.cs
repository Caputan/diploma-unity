using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using UnityEngine;
using PDFWorker;
using UI.LoadingUI;
using UI.TheoryUI;
using UnityEngine.UI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class TheoryController: IInitialization, ICleanData
    {
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly string _pdfPath;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly TheoryUIInitialization _theoryUIInitialization;
        private PDFReader _pdfReader;
       
        public TheoryController(PdfReaderUIInitialization pdfReaderUIInitialization,string pdfPath,
            FileManager fileManager,string pdfStoragePath,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            TheoryUIInitialization theoryUIInitialization,
            LoadingUILogic loadingUILogic)
        {
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _pdfPath = pdfPath;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _theoryUIInitialization = theoryUIInitialization;
            _pdfReader = new PDFReader(
                fileManager,pdfStoragePath,
                _gameContextWithViewsTheory,_pdfReaderUIInitialization,
                loadingUILogic);
        }
        public void Initialization()
        {
            _theoryUIInitialization.SetButtons();
            CreateDocumentLocaly(_pdfPath);
        }
        public void CleanData()
        {
           //_theoryUIInitialization.CleanData();
           UnloadDocument();
        }
        

        private void CreateDocumentLocaly(string pdfPath)
        {
            _pdfReader.RaedFile(pdfPath);
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