using System;
using System.Collections;
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

        public void RaedFile(string inputPdfFile)
        {
            _loadingUILogic.SetActiveLoading(true);
            RaedFileCorutine(inputPdfFile).StartCoroutine(out _);
        }
        
        public IEnumerator RaedFileCorutine(string inputPdfFile)
        {
            
            yield return new WaitForSeconds(1);

            _inputPdfFile = inputPdfFile;
            float numberOfPages = GetNumberOfPages(inputPdfFile);
            var destinationPath = _fileManager.CreateFileFolder(_positionPath+ 
                                                                 "\\"+ 
                                                                 Path.GetFileNameWithoutExtension(_inputPdfFile).
                                                                     Split('\\').Last());
            _gameContextWithViewsTheory.SetNameOfFolder(destinationPath);
            for (float i = 1; i < numberOfPages; i++)
            {
                float paramForText =i*100/numberOfPages;
                float paramForSlider =  Mathf.Clamp01(i / numberOfPages);
                yield return _loadingUILogic.
                    LoadingParams(paramForSlider,Mathf.Round(paramForText)).StartCoroutine(out _);
                //SetLoadingParameter(paramForSlider,Mathf.Round(paramForText));
                yield return ConvertPageToImage(i,destinationPath).StartCoroutine(out _);
            }

            _loadingUILogic.SetActiveLoading(false);
            LoadDocument();
            
            yield return null;
        }
        
        private void LoadDocument()
        {
            _pdfReaderUIInitialization.ReadNextDoc();
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
        public IEnumerator ConvertPageToImage(float pageNumber, string pathToSave)
        {
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
            //тут указан путь куда.
            dev.OutputPath = pathToSave +"\\"+ outImageName;
            dev.Process();
            yield return null;
        }
    }
}