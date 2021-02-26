using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using UnityEngine;
using IDataBase = Interfaces.IDataBase;
using ITable = Interfaces.ITable;

namespace Tables
{
    public class LesionsTable : IDataBase
    {
        public List<ITable> GetAllData(DataContext context)
        {
            Table<Lesions> lesions = context.GetTable<Lesions>();

            var query = from lesion in lesions select lesion;

            List<ITable> lesionsList = new List<ITable>();
            foreach (var lesion in query)
            {
                lesionsList.Add(lesion);
            }

            return lesionsList;
        }
    
        public ITable GetRecordById(DataContext context, int id)
        {
            Table<Lesions> lesions = context.GetTable<Lesions>();

            var query = from lesion in lesions where lesion.Lesion_Id == id select lesion;

            foreach (var lesion in query)
            {
                return lesion;
            }

            return null;
        }

        public void AddNewRecord(DataContext context, string[] lesionParams)
        {
            Table<Lesions> lesions = context.GetTable<Lesions>();

            Lesions newLesion = new Lesions()
            {
                Lesion_Preview = Int32.Parse(lesionParams[0]),
                Lesion_Text = Int32.Parse(lesionParams[1]),
                Lesion_Video = Int32.Parse(lesionParams[2]),
                Lesion_Assembly = Int32.Parse(lesionParams[3])
            };
        
            lesions.InsertOnSubmit(newLesion);
            context.SubmitChanges();
        }
    
        [Table(Name = "Lesions")]
        public class Lesions : ITable
        {
            [Column(Name = "Lesion_Id")]
            public int Lesion_Id { get; set; }
            [Column(Name = "Lesion_Preview")]
            public int Lesion_Preview { get; set; }
            [Column(Name = "Lesion_Text")]
            public int Lesion_Text { get; set; }
            [Column(Name = "Lesion_Video")]
            public int Lesion_Video { get; set; }
            [Column(Name = "Lesion_Assembly")]
            public int Lesion_Assembly { get; set; }
        }
    }
}
