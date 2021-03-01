using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using Diploma.Interfaces;
using ITable = Diploma.Interfaces.ITable;

namespace Diploma.Tables
{
    public class UsersTable : IDataBase
    {
        public List<ITable> GetAllData(DataContext context)
        {
            Table<Users> users = context.GetTable<Users>();

            var query = from user in users select user;

            List<ITable> usersList = new List<ITable>();
            foreach (var user in query)
            {
                usersList.Add(user);
            }

            return usersList;
        }
    
        public ITable GetRecordById(DataContext context, int id)
        {
            Table<Users> users = context.GetTable<Users>();

            var query = from user in users where user.User_Id == id select user;

            foreach (var user in query)
            {
                return user;
            }

            return null;
        }
        public void AddNewRecord(DataContext context, string[] userParams)
        {
            Table<Users> users = context.GetTable<Users>();

            Users newUser = new Users()
            {
                User_Name = userParams[0],
                User_Email = userParams[1],
                User_Password = userParams[2],
                User_Role = userParams[3]
            };
        
            users.InsertOnSubmit(newUser);
            context.SubmitChanges();
        }


        public class Users : ITable
        {
            [Column(Name = "User_Id")]
            public int User_Id { get; set; }
            [Column(Name = "User_Name")]
            public string User_Name { get; set; }
            [Column(Name = "User_Email")]
            public string User_Email { get; set; }
            [Column(Name = "User_Password")]
            public string User_Password { get; set; }
            [Column(Name = "User_Role")]
            public string User_Role { get; set; }
        }
    }
}
