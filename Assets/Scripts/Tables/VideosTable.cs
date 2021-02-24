using UnityEngine;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

public class VideosTable : ITable
{
    public void GetAllData(DataContext context)
    {
        Table<Videos> videos = context.GetTable<Videos>();

        var query = from video in videos select video;

        foreach (var video in query)
        {
            Debug.Log(video.Video_Id);
            Debug.Log(video.Video_Link);
        }
    }
    
    public void GetRecord(DataContext context)
    {
        Table<Videos> videos = context.GetTable<Videos>();

        var query = from video in videos where video.Video_Id == 1 select video;

        foreach (var text in query)
        {
            //return text.Text_Link;
        }
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
public class Videos
{
    [Column(Name = "Video_Id")] 
    public int Video_Id { get; set; }
    [Column(Name = "Video_Link")] 
    public string Video_Link { get; set; }
}
