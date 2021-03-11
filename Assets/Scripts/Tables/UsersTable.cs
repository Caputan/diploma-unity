using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;
using SQLite4Unity3d;

namespace Diploma.Tables
{
    public class UsersTable : IDataBase
    {
        public List<Users> GetAllData<Users>(SQLiteConnection connection)
        {
            List<Users> usersList = new List<Users>();
            usersList = connection.Table<Users>().ToList();
            

            return usersList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            return connection.Table<Users>().FirstOrDefault(x => x.User_Id == id);
        }

        public ITable GetRecordByName(SQLiteConnection connection, string name)
        {
            return connection.Table<Users>().FirstOrDefault(x => x.User_Name == name);
        }

        public void AddNewRecord(SQLiteConnection connection, string[] userParams)
        {
            var newUser = new Users()
            {
                User_Name = userParams[0],
                User_Email = userParams[1],
                User_Password = userParams[2],
                User_Role = userParams[3]
            };
            connection.Insert(newUser);
        }
    }
    

    public class Users : ITable
    {
        [PrimaryKey, AutoIncrement]
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public string User_Role { get; set; }
    }
}
