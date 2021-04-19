using System;
using System.IO;
using System.Linq;
using Diploma.Controllers;
using Diploma.Managers;
using Ghostscript.NET;
using iTextSharp.text.pdf;


namespace PDFWorker
{
    public sealed class PDFReader
    {
        private readonly FileManager _fileManager;
        private readonly string _positionPath;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private string _fileName;
        private string _inputPdfFile;
        
        public PDFReader(FileManager fileManager,string positionPath,GameContextWithViewsTheory gameContextWithViewsTheory)
        {
            //"LocalPDFDocumentsInImages"
            _fileManager = fileManager;
            _positionPath = positionPath;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _fileManager.CreateFileFolder(_positionPath);
        }

        public void RaedFile(string inputPdfFile)
        {
            _inputPdfFile = inputPdfFile;
            var numberOfPages = GetNumberOfPages(inputPdfFile);
            var destinationPath = _fileManager.CreateFileFolder(_positionPath+ 
                                                                 "\\"+ 
                                                                 Path.GetFileNameWithoutExtension(_inputPdfFile).
                                                                     Split('\\').Last());
            _gameContextWithViewsTheory.SetNameOfFolder(destinationPath);
            for (int i = 1; i < numberOfPages; i++)
            {
                ConvertPageToImage(i,destinationPath);
            }
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
        public void ConvertPageToImage(int pageNumber, string pathToSave)
        {
            string outImageName = Path.GetFileNameWithoutExtension(_inputPdfFile);
            outImageName = outImageName+"_"+pageNumber.ToString() + "_.png";
            
            GhostscriptPngDevice dev = new GhostscriptPngDevice(GhostscriptPngDeviceType.Png256);
            dev.GraphicsAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.TextAlphaBits = GhostscriptImageDeviceAlphaBits.V_4;
            dev.ResolutionXY = new GhostscriptImageDeviceResolution(290, 290);
            dev.InputFiles.Add(_inputPdfFile);
            dev.Pdf.FirstPage = pageNumber;
            dev.Pdf.LastPage = pageNumber;
            dev.CustomSwitches.Add("-dDOINTERPOLATE");
            //тут указан путь куда.
            dev.OutputPath = pathToSave +"\\"+ outImageName;
            dev.Process();

        }
    }
}

#region Trash

// StringBuilder text = new StringBuilder();
//
//  if (File.Exists(_fileName))
//  {
//      PdfReader pdfReader = new PdfReader(_fileName);
//
//      for (int page = 1; page <= pdfReader.NumberOfPages; page++)
//      {
//          ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
//          string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);
//
//          currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
//          text.Append(currentText);
//      }
//      pdfReader.Close();
//  }
#endregion