using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;
using SQLite4Unity3d;

namespace Diploma.Tables
{
    public class TypesTable : IDataBase
    {
        public List<Types> GetAllData<Types>(SQLiteConnection connection)
        {
            // Table<Types> types = context.GetTable<Types>();
            //
            // var query = from type in types select type;
            //
            // List<ITable> typesList = new List<ITable>();
            // foreach (var type in query)
            // {
            //     typesList.Add(type);
            // }
            List<Types> typesList = new List<Types>();
            typesList = connection.Table<Types>().ToList();
            
            return typesList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            // Table<Types> types = context.GetTable<Types>();
            //
            // var query = from type in types where type.Type_Id == id select type;
            //
            // foreach (var type in query)
            // {
            //     return type;
            // }

            return connection.Table<Types>().FirstOrDefault(x => x.Type_Id == id);
        }

        public void AddNewRecord(SQLiteConnection connection, string[] typeParams)
        {
            // Table<Types> types = context.GetTable<Types>();
            //
            // Types newType = new Types()
            // {
            //     Type_Id = 4,
            //     Type_Image = arrayForFiles
            // };
            //
            // types.InsertOnSubmit(newType);
            // context.SubmitChanges();
            var newType = new Types()
            {
                Type_Id = 4,
                Type_Image = typeParams[0]
            };
            connection.Insert(newType);
        }
    }


    public class Types : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int Type_Id { get; set; }
        public string Type_Image { get; set; }
    }
}