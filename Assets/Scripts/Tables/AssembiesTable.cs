using UnityEngine;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Mono.Data.Sqlite;
using System.IO;

public class AssemliesTable : ITable
{
    public void GetAllData(DataContext context)
    {
        Table<Assemblies> assemblies = context.GetTable<Assemblies>();

        var query = from assembly in assemblies select assembly;

        foreach (var assembly in query)
        {
            Debug.Log(assembly.Assembly_Id);
            Debug.Log(assembly.Assembly_Link);
        }
    }
    
    public void GetRecord(DataContext context)
    {
        Table<Assemblies> assemblies = context.GetTable<Assemblies>();

        var query = from assembly in assemblies where assembly.Assembly_Id == 1 select assembly;

        foreach (var assembly in query)
        {
            //return text.Text_Link;
        }
    }

    public void AddData(DataContext context)
    {
        throw new System.NotImplementedException();
    }
}


[Table(Name = "Assemblies")]
public class Assemblies
{
    [Column(Name = "Assembly_Id")] 
    public int Assembly_Id { get; set; }
    [Column(Name = "Assembly_Link")] 
    public string Assembly_Link { get; set; }
}
