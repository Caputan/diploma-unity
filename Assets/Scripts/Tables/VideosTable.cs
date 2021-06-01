using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;
using SQLite4Unity3d;

namespace Diploma.Tables
{
    public class VideosTable : IDataBase
    {
        public List<Videos> GetAllData<Videos>(SQLiteConnection connection)
        {
            List<Videos> videosList = new List<Videos>();
            videosList = connection.Table<Videos>().ToList();
            

            return videosList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            return connection.Table<Videos>().FirstOrDefault(x => x.Video_Id == id);
        }
        
        public ITable GetRecordByName(SQLiteConnection connection, string name)
        {
            return null;
        }

        public void AddNewRecord(SQLiteConnection connection, string[] videoParams)
        {
            var newVideo = new Videos()
            {
                Video_Link = videoParams[0]
            };
            connection.Insert(newVideo);
        }

        public void UpdateRecordById(SQLiteConnection connection, int id, string[] paramsToChange)
        {
            
        }

        public void DeleteLastRecord(SQLiteConnection connection, int id)
        {
            connection.Delete<Videos>(id);
        }
    }


    public class Videos : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Video_Id { get; set; }
        public string Video_Link { get; set; }
    }
}