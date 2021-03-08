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
            // Table<Users> users = context.GetTable<Users>();
            //
            // var query = from user in users select user;
            //
            // List<ITable> usersList = new List<ITable>();
            // foreach (var user in query)
            // {
            //     usersList.Add(user);
            // }
            List<Users> usersList = new List<Users>();
            usersList = connection.Table<Users>().ToList();
            

            return usersList;
        }
    
        public ITable GetRecordById(SQLiteConnection connection, int id)
        {
            // Table<Users> users = context.GetTable<Users>();
            //
            // var query = from user in users where user.User_Id == id select user;
            //
            // foreach (var user in query)
            // {
            //     return user;
            // }

            return connection.Table<Users>().FirstOrDefault(x => x.User_Id == id);
        }
        public void AddNewRecord(SQLiteConnection connection, string[] userParams, byte[] arrayForFiles)
        {
            // Table<Users> users = context.GetTable<Users>();
            //
            // Users newUser = new Users()
            // {
            //     User_Name = userParams[0],
            //     User_Email = userParams[1],
            //     User_Password = userParams[2],
            //     User_Role = userParams[3]
            // };
            //
            // users.InsertOnSubmit(newUser);
            // context.SubmitChanges();
            var newUser = new Users()
            {
                User_Id = 4,
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
        public int User_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }
        public string User_Password { get; set; }
        public string User_Role { get; set; }
    }
}
