using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;

namespace Diploma.Tables
{
    public class TypesTable : IDataBase
    {
        public List<ITable> GetAllData(DataContext context)
        {
            Table<Types> types = context.GetTable<Types>();

            var query = from type in types select type;
    
            List<ITable> typesList = new List<ITable>();
            foreach (var type in query)
            {
                typesList.Add(type);
            }

            return typesList;
        }
    
        public ITable GetRecordById(DataContext context, int id)
        {
            Table<Types> types = context.GetTable<Types>();

            var query = from type in types where type.Type_Id == id select type;

            foreach (var type in query)
            {
                return type;
            }

            return null;
        }

        public void AddNewRecord(DataContext context, string[] assemblyParams)
        {
            // Table<Types> assemblies = context.GetTable<Types>();
            //
            // Types newType = new Types()
            // {
            //     Type_Image = assemblyParams[0]
            // };
            //
            // assemblies.InsertOnSubmit(newType);
            // context.SubmitChanges();
        }
    }

    [Table(Name = "Types")]
    public class Types : ITable
    {
        [Column(Name = "Type_Id")] 
        public int Type_Id { get; set; }
        [Column(Name = "Type_Image")] 
        public string Type_Image { get; set; }
    }
}