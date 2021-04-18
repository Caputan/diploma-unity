using Diploma.Interfaces;
using Diploma.Managers;
using UnityEngine;
using PDFWorker;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class TheoryController: IInitialization
    {
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly string _pdfPath;
        private readonly string _pdfStoragePath;
        private PDFReader _pdfReader;
        public TheoryController(PdfReaderUIInitialization pdfReaderUIInitialization,string pdfPath,FileManager fileManager,string pdfStoragePath)
        {
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _pdfPath = pdfPath;
            _pdfStoragePath = pdfStoragePath;
            _pdfReader = new PDFReader(fileManager,_pdfStoragePath);
        }
        public void Initialization()
        {
            CreateDocumentLocaly(_pdfPath);
            LoadDocument();
        }

        private void CreateDocumentLocaly(string pdfPath)
        {
            _pdfReader.RaedFile(pdfPath);
        }

        private void RemoveDocumentPng()
        {
            _pdfReader.DeleteStorage();
        }
        
        private void LoadDocument()
        {
            _pdfReaderUIInitialization.ReadANewPdfDocument(_pdfStoragePath);
        }
    }
}