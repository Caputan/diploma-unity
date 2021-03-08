using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using UnityEngine;
using ITable = Diploma.Interfaces.ITable;
using SQLite4Unity3d;
using UnityEngine.UI;

namespace Diploma.Tables
{
    public class TextsTable : IDataBase
    {
        public List<Texts> GetAllData<Texts>(SQLiteConnection connection)
        {
            // Table<Texts> texts = context.GetTable<Texts>();
            //
            // var query = from text in texts select text;
            //
            // List<ITable> textsList = new List<ITable>();
            // foreach (var text in query)
            // {
            //     Debug.Log(text.Text_Link);
            //     textsList.Add(text);
            // }
            List<Texts> textsList = new List<Texts>();
            textsList = connection.Table<Texts>().ToList();
            

            return textsList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            // Table<Texts> texts = context.GetTable<Texts>();
            //
            // var query = from text in texts where text.Text_Id == id select text;
            //
            // foreach (var text in query)
            // {
            //     return text;
            // }

            return connection.Table<Texts>().FirstOrDefault(x => x.Text_Id == id);
        }

        public void AddNewRecord(SQLiteConnection connection, string[] textParams, byte[] arrayForFiles)
        {
            // Table<Texts> texts = context.GetTable<Texts>();
            //
            // Texts newText = new Texts()
            // {
            //     Text_Link = textParams[0]
            // };
            //
            // texts.InsertOnSubmit(newText);
            // context.SubmitChanges();
            var newText = new Texts()
            {
                Text_Id = 5,
                Text_Link = textParams[0]
            };
            connection.Insert(newText);
        }
        
    }
    

    public class Texts : ITable
    {
        public int Text_Id { get; set; }
        public string Text_Link { get; set; }
    }
}
