﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public sealed class TheoryController: IInitialization
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
                _gameContextWithViewsTheory,
                loadingUILogic);
        }
        public void Initialization()
        {
            _theoryUIInitialization.SetButtons();
            Dictionary<int, string> obj = new Dictionary<int, string>();
            obj.Add(-1,_pdfPath.Text_Link);
            rewriteDictionary?.Invoke(obj);
            //CreateDocumentLocaly(obj);
            //LoadDocumentTheory();
        }

        public void LoadDocumentTheory()
        {
            _pdfReaderUIInitialization.ReadNextDoc(-1);
        }

        public void CreateDocumentLocaly(
            int id, 
            string obj,
            List<CoroutineController> сoroutineController,
            int hand
            )
        {
            Debug.Log("id: "+id + " string: "+obj);
            _pdfReader.RaedFile(id,obj, сoroutineController,hand);
        }

        public void RemoveDocumentPng()
        {
           // _pdfReader.DeleteStorage();
        }

    }
}