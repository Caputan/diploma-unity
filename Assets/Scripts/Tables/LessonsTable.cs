using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;
using SQLite4Unity3d;

namespace Diploma.Tables
{
    public class LessonsTable : IDataBase
    {
        public List<Lessons> GetAllData<Lessons>(SQLiteConnection connection)
        {
            List<Lessons> lessonsList = new List<Lessons>();
            lessonsList = connection.Table<Lessons>().ToList();
            

            return lessonsList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            return connection.Table<Lessons>().FirstOrDefault(x => x.Lesson_Id == id);
        }

        public ITable GetRecordByName(SQLiteConnection connection, string name)
        {
            return null;
        }

        public void AddNewRecord(SQLiteConnection connection, string[] lessonParams)
        {
            var newLesson = new Lessons()
            {
                Lesson_Preview = lessonParams[0],
                Lesson_Text_Id = Int32.Parse(lessonParams[1]),
                Lesson_Video_Id = Int32.Parse(lessonParams[2]),
                Lesson_Assembly_Id = Int32.Parse(lessonParams[3])
            };
            connection.Insert(newLesson);
        }
        
    }
    
    public class Lessons : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Lesson_Id { get; set; }
        public string Lesson_Preview { get; set; }
        public int Lesson_Text_Id { get; set; }
        public int Lesson_Video_Id { get; set; }
        public int Lesson_Assembly_Id { get; set; }
    }
}
