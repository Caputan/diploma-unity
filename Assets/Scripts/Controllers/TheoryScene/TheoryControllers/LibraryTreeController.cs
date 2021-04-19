using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Managers;

namespace Controllers.TheoryScene.TheoryControllers
{
    public class LibraryTreeController: IInitialization, ICleanData
    {

        public LibraryTreeController(PdfReaderUIInitialization pdfReaderUIInitialization,string pdfPath,
            FileManager fileManager,string pdfStoragePath,
            GameContextWithViewsTheory gameContextWithViewsTheory,
            TheoryUIInitialization theoryUIInitialization)
        {
            
        }
        public void Initialization()
        {
            
        }

        public void CleanData()
        {
            throw new System.NotImplementedException();
        }
    }
}