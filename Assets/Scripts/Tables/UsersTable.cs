﻿using UnityEngine;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Mono.Data.Sqlite;
using System.IO;

public class UsersTable : ITable
{
    public void GetAllData(DataContext context)
    {
        Table<Users> users = context.GetTable<Users>();

        var query = from user in users select user;

        foreach (var user in query)
        {
            Debug.Log(user.User_Name);
            Debug.Log(user.User_Role);
        }
    }
    
    public void GetRecord(DataContext context)
    {
        Table<Users> users = context.GetTable<Users>();

        var query = from user in users select user;

        foreach (var user in query)
        {
            
        }
    }

    public void AddData(DataContext context)
    {
        throw new System.NotImplementedException();
    }


    public class Users
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
