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
            List<Texts> textsList = new List<Texts>();
            textsList = connection.Table<Texts>().ToList();
            

            return textsList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            return connection.Table<Texts>().FirstOrDefault(x => x.Text_Id == id);
        }

        public ITable GetRecordByName(SQLiteConnection connection, string name)
        {
            return null;
        }

        public void AddNewRecord(SQLiteConnection connection, string[] textParams)
        {
            var newText = new Texts()
            {
                Text_Link = textParams[0]
            };
            connection.Insert(newText);
        }

        public void UpdateRecordById(SQLiteConnection connection, int id, string[] paramsToChange)
        {
            
        }

        public void DeleteLastRecord(SQLiteConnection connection, int id)
        {
            connection.Delete<Texts>(id);
        }
    }
    

    public class Texts : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Text_Id { get; set; }
        public string Text_Link { get; set; }
    }
}
