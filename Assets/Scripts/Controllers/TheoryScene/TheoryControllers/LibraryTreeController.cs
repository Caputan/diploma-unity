using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using PDFWorker;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class LibraryTreeController: ICleanData
    {
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private PDFReader _pdfReader;
        public LibraryTreeController(PdfReaderUIInitialization pdfReaderUIInitialization,
            FileManager fileManager,string pdfStoragePath,
            GameContextWithViewsTheory gameContextWithViewsTheory
            )
        {
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _pdfReader = new PDFReader(fileManager,pdfStoragePath,_gameContextWithViewsTheory);
        }
        public void Initialization(string pdfPath)
        {
            CreateDocumentLocaly(pdfPath);
            LoadDocument();
        }
        public void CleanData()
        {
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

        private void LoadDocument()
        {
            _pdfReaderUIInitialization.ReadANewPdfDocument();
        }

        private void UnloadDocument()
        {
            _pdfReaderUIInitialization.UnloadDocument();
        }
    }
}