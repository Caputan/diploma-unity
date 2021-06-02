using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Controllers.TheoryScene.TheoryControllers;
using Coroutine;
using Diploma.Controllers;
using Diploma.Enums;
using Diploma.Managers;
using Ghostscript.NET;
using iTextSharp.text.pdf;
using Tools;
using UI.LoadingUI;
using UnityEngine;


namespace PDFWorker
{
    public sealed class PDFReader
    {
        private readonly FileManager _fileManager;
        private readonly string _positionPath;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private readonly LoadingUILogic _loadingUILogic;
        private string _fileName;
        private string _inputPdfFile;
        private int i;

        public PDFReader(FileManager fileManager,
            string positionPath,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            LoadingUILogic loadingUILogic)
        {
            _fileManager = fileManager;
            _positionPath = positionPath;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _loadingUILogic = loadingUILogic;
            _fileManager.CreateFileFolder(_positionPath);
        }

        public void RaedFile(
            int id,
            string strings,
            List<CoroutineController> сoroutineController,
            int hand
            )
        {
            RaedFileCorutine(id, strings, сoroutineController,hand);
        }
        
       
       private void RaedFileCorutine(
           int id,
           string inputPdfFile,
           List<CoroutineController> сoroutineController,
           int hand
           )
       {
            
      
            
            _inputPdfFile = inputPdfFile;
            
            float numberOfPages = GetNumberOfPages(_inputPdfFile);
            var destinationPath = _fileManager.CreateFileFolder(_positionPath+ 
                                                                 "\\"+ 
                                                                 Path.GetFileNameWithoutExtension(_inputPdfFile).
                                                                     Split('\\').Last());
            _gameContextWithViewsTheory.SetNameOfFolder(id, destinationPath);
           
            i = 1;
            ConvertPageToImage(i, destinationPath,numberOfPages,сoroutineController,hand).
                StartCoroutine(out _,out _);
            
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
       private IEnumerator ConvertPageToImage(
            float pageNumber,
            string pathToSave,
            float numberOfPages,
            List<CoroutineController> сoroutineController,
            int hand
            )
        {
            yield return new WaitForEndOfFrame();
            
            if (pageNumber > numberOfPages)
            {
                Debug.Log("Сейчас корутина будет остановлена");
                сoroutineController[hand].state = CoroutineState.Finished;
                yield break;
            }
            Debug.Log(pageNumber);
            yield return new WaitForEndOfFrame();
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
            float paramForText =pageNumber*100/numberOfPages;
            float paramForSlider =  Mathf.Clamp01(pageNumber / numberOfPages);
            _loadingUILogic.LoadingParams(paramForSlider, Mathf.Round(paramForText),
                _inputPdfFile.Split('/').Last().Split('\\').Last());
            yield return new WaitForEndOfFrame();
            i++;
            
            
            ConvertPageToImage(i, pathToSave, numberOfPages,сoroutineController,hand).StartCoroutine(out _,out var routine);
            if (pageNumber == numberOfPages)
            {
                сoroutineController.Add(routine);
            }
            yield return null;
        }
    }
}