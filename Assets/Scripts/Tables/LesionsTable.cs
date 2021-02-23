using UnityEngine;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Mono.Data.Sqlite;
using System.IO;

public class LesionTable : ITable
{
    public void GetAllData(DataContext context)
    {
        Table<Lesions> lesions = context.GetTable<Lesions>();

        var query = from lesion in lesions select lesion;

        foreach (var lesion in query)
        {
            Debug.Log(lesion.Lesion_Id);
            Debug.Log(lesion.Lesion_Text);
        }
    }
    
    public void GetRecord(DataContext context)
    {
        Table<Lesions> lesions = context.GetTable<Lesions>();

        var query = from lesion in lesions select lesion;

        foreach (var user in query)
        {
            
        }
    }

    public void AddData(DataContext context)
    {
        throw new System.NotImplementedException();
    }
    
    [Table(Name = "Lesions")]
    public class Lesions
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
