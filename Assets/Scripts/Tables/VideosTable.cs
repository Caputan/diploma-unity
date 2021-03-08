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
            // Table<Videos> videos = context.GetTable<Videos>();
            //
            // var query = from video in videos select video;
            ///
            // List<ITable> videosList = new List<ITable>();
            // foreach (var video in query)
            // {
            //     videosList.Add(video);
            // }
            List<Videos> videosList = new List<Videos>();
            videosList = connection.Table<Videos>().ToList();
            

            return videosList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            // Table<Videos> videos = context.GetTable<Videos>();
            //
            // var query = from video in videos where video.Video_Id == id select video;
            //
            // foreach (var video in query)
            // {
            //     return video;
            // }

            return connection.Table<Videos>().FirstOrDefault(x => x.Video_Id == id);
        }

        public void AddNewRecord(SQLiteConnection connection, string[] videoParams, byte[] arrayForFiles)
        {
            // Table<Videos> videos = context.GetTable<Videos>();
            //
            // Videos newVideo = new Videos()
            // {
            //     Video_Link = videoParams[0]
            // };
            //
            // videos.InsertOnSubmit(newVideo);
            // context.SubmitChanges();
            var newVideo = new Videos()
            {
                Video_Id = 4,
                Video_Link = videoParams[0]
            };
            connection.Insert(newVideo);
        }
    }


    public class Videos : ITable
    {
        public int Video_Id { get; set; }
        public string Video_Link { get; set; }
    }
}