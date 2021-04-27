using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Controllers.TheoryScene.TheoryControllers;
using Coroutine;
using Diploma.Controllers;
using Diploma.Managers;
using Ghostscript.NET;
using iTextSharp.text.pdf;
using UI.LoadingUI;
using UnityEngine;


namespace PDFWorker
{
    public sealed class PDFReader
    {
        private readonly FileManager _fileManager;
        private readonly string _positionPath;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly LoadingUILogic _loadingUILogic;
        private string _fileName;
        private string _inputPdfFile;

        public PDFReader(FileManager fileManager,
            string positionPath,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            PdfReaderUIInitialization pdfReaderUIInitialization,
            LoadingUILogic loadingUILogic)
        {
            _fileManager = fileManager;
            _positionPath = positionPath;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _loadingUILogic = loadingUILogic;
            _fileManager.CreateFileFolder(_positionPath);
        }

        public void RaedFile( int id, string strings)
        {
            RaedFileCorutine(id, strings); //.StartCoroutine(out _);
        }
        
       // public IEnumerator RaedFileCorutine(int id, string inputPdfFile)
       private void RaedFileCorutine(int id, string inputPdfFile)
       {
            
           // yield return new WaitForEndOfFrame();

            _inputPdfFile = inputPdfFile;
            float numberOfPages = GetNumberOfPages(inputPdfFile);
            var destinationPath = _fileManager.CreateFileFolder(_positionPath+ 
                                                                 "\\"+ 
                                                                 Path.GetFileNameWithoutExtension(_inputPdfFile).
                                                                     Split('\\').Last());
            _gameContextWithViewsTheory.SetNameOfFolder(id,destinationPath);
            Debug.Log("File is going to read: "+ _inputPdfFile);
            for (float i = 1; i <= numberOfPages; i++)
            {
                float paramForText =i*100/numberOfPages;
                float paramForSlider =  Mathf.Clamp01(i / numberOfPages);
                _loadingUILogic.
                    LoadingParams(paramForSlider,Mathf.Round(paramForText),
                        _inputPdfFile.Split('/').Last());//.StartCoroutine(out _);
                ConvertPageToImage(i, destinationPath); //.StartCoroutine(out _);
            }
            
            // yield return null;
       }
        
        

        public void DeleteStorage()
        {
            _fileManager.DeleteFolder(_positionPath);
        }
        
        private static int GetNumberOfPages(String FilePath)
        {
            PdfReader pdfReader = new PdfReader(FilePath); 
            return pdfReader.NumberOfPages; 
        }
        //public IEnumerator ConvertPageToImage(float pageNumber, string pathToSave)
        public void ConvertPageToImage(float pageNumber, string pathToSave)
        {
            //yield return new WaitForEndOfFrame();
            string outImageName = Path.GetFileNameWithoutExtension(_inputPdfFile);
            outImageName = outImageName+"_"+pageNumber.ToString() + "_.png";
            
            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png256);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(594, 846);
            dev.InputFiles.Add(_inputPdfFile);
            dev.Pdf.FirstPage = (int)pageNumber;
            dev.Pdf.LastPage = (int)pageNumber;
            dev.CustomSwitches.Add("-dDOINTERPOLATE");
            dev.OutputPath = pathToSave +"\\"+ outImageName;
            dev.Process();
            //yield return null;
        }
    }
}