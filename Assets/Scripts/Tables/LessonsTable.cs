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
        public List<ITable> GetAllData(SQLiteConnection connection)
        {
            // Table<Lessons> lessons = context.GetTable<Lessons>();
            //
            // var query = from lesson in lessons select lesson;
            //
            // List<ITable> lessonsList = new List<ITable>();
            // foreach (var lesson in query)
            // {
            //     lessonsList.Add(lesson);
            // }
            //
            // return lessonsList;
            List<ITable> lessonsList = new List<ITable>();
            var query = connection.Table<Lessons>().ToArray();
            foreach (var lesson in query)
            {
                lessonsList.Add(lesson);
            }

            return lessonsList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            // Table<Lessons> lessons = context.GetTable<Lessons>();
            //
            // var query = from lesson in lessons where lesson.Lesson_Id == id select lesson;
            //
            // foreach (var lesson in query)
            // {
            //     return lesson;
            // }

            return connection.Table<Lessons>().FirstOrDefault(x => x.Lesson_Id == id);
        }

        public void AddNewRecord(SQLiteConnection connection, string[] lessonParams, byte[] arrayForFiles)
        {
            // Table<Lessons> lessons = context.GetTable<Lessons>();
            //
            // Lessons newLesson = new Lessons()
            // {
            //     Lesson_Preview = Int32.Parse(lessonParams[0]),
            //     Lesson_Text_Id = Int32.Parse(lessonParams[1]),
            //     Lesson_Video_Id = Int32.Parse(lessonParams[2]),
            //     Lesson_Assembly_Id = Int32.Parse(lessonParams[3])
            // };
            //
            // lessons.InsertOnSubmit(newLesson);
            // context.SubmitChanges();
            var newLesson = new Lessons()
            {
                Lesson_Id = 4,
                Lesson_Preview = Int32.Parse(lessonParams[0]),
                Lesson_Text_Id = Int32.Parse(lessonParams[1]),
                Lesson_Video_Id = Int32.Parse(lessonParams[2]),
                Lesson_Assembly_Id = Int32.Parse(lessonParams[3])
            };
            connection.Insert(newLesson);
        }
        
    }
    
    public class Lessons : ITable
    {
        public int Lesson_Id { get; set; }
        public int Lesson_Preview { get; set; }
        public int Lesson_Text_Id { get; set; }
        public int Lesson_Video_Id { get; set; }
        public int Lesson_Assembly_Id { get; set; }
    }
}
