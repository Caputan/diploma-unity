using UnityEngine;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Mono.Data.Sqlite;
using System.IO;

public class TextTable : ITable
{
    public void GetAllData(DataContext context)
    {
        Table<Texts> texts = context.GetTable<Texts>();

        var query = from text in texts select text;

        foreach (var text in query)
        {
            Debug.Log(text.Text_Id);
            Debug.Log(text.Text_Link);
        }
    }
    
    public void GetRecord(DataContext context)
    {
        Table<Texts> texts = context.GetTable<Texts>();

        var query = from text in texts where text.Text_Id == 1 select text;

        foreach (var text in query)
        {
            //return text.Text_Link;
        }
    }

    public void AddNewRecord(DataContext context, string[] textParams)
    {
        Table<Texts> texts = context.GetTable<Texts>();

        Texts newText = new Texts()
        {
            Text_Link = textParams[0]
        };
        
        texts.InsertOnSubmit(newText);
        context.SubmitChanges();
    }


    [Table(Name = "Texts")]
    public class Texts
    {
        [Column(Name = "Text_Id")] 
        public int Text_Id { get; set; }
        [Column(Name = "Text_Link")] 
        public string Text_Link { get; set; }
    }
}
