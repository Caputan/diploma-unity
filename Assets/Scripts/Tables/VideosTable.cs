using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Interfaces;
using UnityEngine;
using ITable = Interfaces.ITable;

namespace Tables
{
    public class VideosTable : IDataBase
    {
        public List<ITable> GetAllData(DataContext context)
        {
            Table<Videos> videos = context.GetTable<Videos>();

            var query = from video in videos select video;

            List<ITable> videosList = new List<ITable>();
            foreach (var video in query)
            {
                videosList.Add(video);
            }

            return videosList;
        }
    
        public ITable GetRecordById(DataContext context, int id)
        {
            Table<Videos> videos = context.GetTable<Videos>();

            var query = from video in videos where video.Video_Id == id select video;

            foreach (var video in query)
            {
                return video;
            }

            return null;
        }

        public void AddNewRecord(DataContext context, string[] videoParams)
        {
            Table<Videos> videos = context.GetTable<Videos>();

            Videos newVideo = new Videos()
            {
                Video_Link = videoParams[0]
            };
        
            videos.InsertOnSubmit(newVideo);
            context.SubmitChanges();
        }
    }

    [Table(Name = "Videos")]
    public class Videos : ITable
    {
        [Column(Name = "Video_Id")] 
        public int Video_Id { get; set; }
        [Column(Name = "Video_Link")] 
        public string Video_Link { get; set; }
    }
}