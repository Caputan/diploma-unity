using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;

namespace Diploma.Tables
{
    public class AssemliesTable : IDataBase
    {
        public List<ITable> GetAllData(DataContext context)
        {
            Table<Assemblies> assemblies = context.GetTable<Assemblies>();

            var query = from assembly in assemblies select assembly;
            List<ITable> assembliesList = new List<ITable>();
            foreach (var assembly in query)
            {
                assembliesList.Add(assembly);
            }

            return assembliesList;
        }
    
        public ITable GetRecordById(DataContext context, int id)
        {
            Table<Assemblies> assemblies = context.GetTable<Assemblies>();

            var query = from assembly in assemblies where assembly.Assembly_Id == id select assembly;

            foreach (var assembly in query)
            {
                return assembly;
            }

            return null;
        }

        public void AddNewRecord(DataContext context, string[] assemblyParams)
        {
            Table<Assemblies> assemblies = context.GetTable<Assemblies>();

            Assemblies newAssembly = new Assemblies()
            {
                Assembly_Link = assemblyParams[0]
            };
        
            assemblies.InsertOnSubmit(newAssembly);
            context.SubmitChanges();
        }
    }


    [Table(Name = "Assemblies")]
    public class Assemblies : ITable
    {
        [Column(Name = "Assembly_Id")] 
        public int Assembly_Id { get; set; }
        [Column(Name = "Assembly_Link")] 
        public string Assembly_Link { get; set; }
    }
}