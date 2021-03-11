using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;
using SQLite4Unity3d;

namespace Diploma.Tables
{
    public class AssemliesTable : IDataBase
    {
        public List<Assemblies> GetAllData<Assemblies>(SQLiteConnection connection)
        {
            List<Assemblies> assemliesList = new List<Assemblies>();
            assemliesList = connection.Table<Assemblies>().ToList();
            

            return assemliesList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            // Table<Assemblies> assemblies = context.GetTable<Assemblies>();
            //
            // var query = from assembly in assemblies where assembly.Assembly_Id == id select assembly;
            //
            // foreach (var assembly in query)
            // {
            //     return assembly;
            // }
            //
            return connection.Table<Assemblies>().FirstOrDefault(x => x.Assembly_Id == id);
        }

        public ITable GetRecordByName(SQLiteConnection connection, string name)
        {
            return null;
        }

        public void AddNewRecord(SQLiteConnection connection, string[] assemblyParams)
        {
            var newAssembly = new Assemblies()
            {
                Assembly_Link = assemblyParams[0]
            };
            connection.Insert(newAssembly);
        }
    }



    public class Assemblies : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Assembly_Id { get; set; }
        public string Assembly_Link { get; set; }
    }
}