using System.Collections.Generic;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using Diploma.Tables;
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
        private readonly Texts _pdfPath;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly TheoryUIInitialization _theoryUIInitialization;
        private PDFReader _pdfReader;
       
        public TheoryController(PdfReaderUIInitialization pdfReaderUIInitialization,Texts pdfPath,
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
            Dictionary<int, string> obj = new Dictionary<int, string>();
            obj.Add(_pdfPath.Text_Id,_pdfPath.Text_Link);
            CreateDocumentLocaly(obj);
            LoadDocumentTheory();
        }
        public void CleanData()
        {
           //_theoryUIInitialization.CleanData();
           UnloadDocument();
        }
        
        private void LoadDocumentTheory()
        {
            _pdfReaderUIInitialization.ReadNextDoc(0);
        }

        private void CreateDocumentLocaly(Dictionary<int, string> obj)
        {
            _pdfReader.RaedFile(obj);
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