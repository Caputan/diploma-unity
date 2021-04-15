using Diploma.Interfaces;
using UnityEngine;

namespace Controllers.TheoryScene.TheoryControllers
{
    public sealed class TheoryController: IInitialization
    {
        private readonly PdfReaderUIInitialization _pdfReaderUIInitialization;
        private readonly string _pdfPath;

        public TheoryController(PdfReaderUIInitialization pdfReaderUIInitialization,string pdfPath)
        {
            _pdfReaderUIInitialization = pdfReaderUIInitialization;
            _pdfPath = pdfPath;
        }
        public void Initialization()
        {
            LoadDocument();
        }

        private void LoadDocument()
        {
            _pdfReaderUIInitialization.ReadANewPdfDocument(_pdfPath);
        }

        private void UnLoadDocument()
        {
            
        }
    }
}