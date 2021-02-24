using System;
using UnityEngine;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
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
