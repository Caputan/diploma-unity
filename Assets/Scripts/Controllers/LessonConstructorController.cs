using System.Collections.Generic;
using System.Linq;
using Diploma.Controllers;
using Diploma.Interfaces;
using Diploma.Tables;

namespace Controllers
{
    public class LessonConstructorController: IInitialization
    {
        private readonly DataBaseController _dataBaseController;
        private readonly List<IDataBase> _tables;

        public LessonConstructorController(
            DataBaseController dataBaseController,
            List<IDataBase>  tables)
        {
            _dataBaseController = dataBaseController;
            _tables = tables; 
            //string[] infoForLesson = new string[5];
            // infoForLesson[3] = _dataBaseController.GetDataFromTable<Assemblies>().Last().Assembly_Id.ToString();
            // infoForLesson[1] = _dataBaseController.GetDataFromTable<Texts>().Last().Text_Id.ToString();  
            // infoForLesson[2] = _dataBaseController.GetDataFromTable<Videos>().Last().Video_Id.ToString();
            // _dataBaseController.SetTable(_tables[1]);
            // _dataBaseController.AddNewRecordToTable(infoForLesson);   
        }
        public void Initialization() { }

        public void CreateALesson()
        {
            string[] lessonPacked = new string[5];
            lessonPacked[0] = "0"; // add screens
            lessonPacked[3] = _dataBaseController.GetDataFromTable<Assemblies>().Last().Assembly_Id.ToString();
            lessonPacked[1] = _dataBaseController.GetDataFromTable<Texts>().Last().Text_Id.ToString();  
            lessonPacked[2] = _dataBaseController.GetDataFromTable<Videos>().Last().Video_Id.ToString();
            lessonPacked[4] = "0"; // add type
            _dataBaseController.SetTable(_tables[1]);
            _dataBaseController.AddNewRecordToTable(lessonPacked); 
        }
    }
}