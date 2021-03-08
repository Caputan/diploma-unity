using Diploma.Interfaces;
using ListOfLessons;

namespace Controllers
{
    public sealed class ListOfLessonsController: IInitialization,ICleanData
    {
        private readonly ListOfLessonsView _listOfLessonsView;

        public ListOfLessonsController(ListOfLessonsView listOfLessonsView)
        {
            _listOfLessonsView = listOfLessonsView;
        }

        public void Initialization()
        {
            throw new System.NotImplementedException();
        }

        public void CleanData()
        {
            throw new System.NotImplementedException();
        }
    }
}