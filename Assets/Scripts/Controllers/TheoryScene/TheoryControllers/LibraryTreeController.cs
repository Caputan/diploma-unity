using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;
using PDFWorker;
using UI.LoadingUI;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class LibraryTreeController: ICleanData
    {
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly GameContextWithViewsTheory _gameContextWithViewsTheory;
        private PDFReader _pdfReader;
        public LibraryTreeController(PdfReaderUIInitialization pdfReaderUIInitialization,
            FileManager fileManager,string pdfStoragePath,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            LoadingUILogic loadingUILogic
            )
        {
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _gameContextWithViewsTheory = gameContextWithViewsTheory;
            _pdfReader = new PDFReader(fileManager,pdfStoragePath,
                _gameContextWithViewsTheory,_pdfReaderUIInitialization,
                loadingUILogic);
        }
        public void Initialization(string pdfPath)
        {
            CreateDocumentLocaly(pdfPath);
            
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

        

        private void UnloadDocument()
        {
            _pdfReaderUIInitialization.UnloadDocument();
        }
    }
}