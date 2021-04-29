using System;
using System.Collections;
using System.Collections.Generic;
using Coroutine;
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
        public event Action<Dictionary<int, string>> rewriteDictionary;
            
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
            obj.Add(0,_pdfPath.Text_Link);
            rewriteDictionary?.Invoke(obj);
            //CreateDocumentLocaly(obj);
            //LoadDocumentTheory();
        }
        public void CleanData()
        {
           //_theoryUIInitialization.CleanData();
           UnloadDocument();
        }
        
        public void LoadDocumentTheory()
        {
            _pdfReaderUIInitialization.ReadNextDoc(0);
        }

        public void CreateDocumentLocaly(
            int id, 
            string obj,
            List<CoroutineController> сoroutineController,
            int hand
            )
        {
            _pdfReader.RaedFile(id,obj, сoroutineController,hand);
        }

        public void RemoveDocumentPng()
        {
           // _pdfReader.DeleteStorage();
        }
        

        private void UnloadDocument()
        {
            _pdfReaderUIInitialization.UnloadDocument();
        }
        
    }
}