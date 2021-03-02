using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;

namespace Diploma.Tables
{
    public class LessonsTable : IDataBase
    {
        public List<ITable> GetAllData(DataContext context)
        {
            Table<Lessons> lessons = context.GetTable<Lessons>();

            var query = from lesson in lessons select lesson;

            List<ITable> lessonsList = new List<ITable>();
            foreach (var lesson in query)
            {
                lessonsList.Add(lesson);
            }

            return lessonsList;
        }
    
        public ITable GetRecordById(DataContext context, int id)
        {
            Table<Lessons> lessons = context.GetTable<Lessons>();

            var query = from lesson in lessons where lesson.Lesson_Id == id select lesson;

            foreach (var lesson in query)
            {
                return lesson;
            }

            return null;
        }

        public void AddNewRecord(DataContext context, string[] lessonParams)
        {
            Table<Lessons> lessons = context.GetTable<Lessons>();

            Lessons newLesson = new Lessons()
            {
                Lesson_Preview = Int32.Parse(lessonParams[0]),
                Lesson_Text = Int32.Parse(lessonParams[1]),
                Lesson_Video = Int32.Parse(lessonParams[2]),
                Lesson_Assembly = Int32.Parse(lessonParams[3])
            };
        
            lessons.InsertOnSubmit(newLesson);
            context.SubmitChanges();
        }
    
        [Table(Name = "Lessons")]
        public class Lessons : ITable
        {
            [Column(Name = "Lesson_Id")]
            public int Lesson_Id { get; set; }
            [Column(Name = "Lesson_Preview")]
            public int Lesson_Preview { get; set; }
            [Column(Name = "Lesson_Text")]
            public int Lesson_Text { get; set; }
            [Column(Name = "Lesson_Video")]
            public int Lesson_Video { get; set; }
            [Column(Name = "Lesson_Assembly")]
            public int Lesson_Assembly { get; set; }
        }
    }
}
