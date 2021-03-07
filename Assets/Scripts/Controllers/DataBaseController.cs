﻿using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using Diploma.Interfaces;
using Mono.Data.Sqlite;
using Diploma.Tables;
using SQLite4Unity3d;
using UnityEngine;
using ITable = Diploma.Interfaces.ITable;

namespace Controllers
{
    public class DataBaseController : IInitialization
    {
        private const string _dbName = "MachinePartsDB.bytes";
        private static DataContext _context;
        private SQLiteConnection _connection;

        private IDataBase _activeTable;
    
    
        // Start is called before the first frame update
        public DataBaseController()
        {
            var dbPath = Path.Combine(Application.streamingAssetsPath, _dbName);
            // _connection = new SqliteConnection("Data Source=" + dbPath);
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite);
        }

        public void SetTable(IDataBase table)
        {
            this._activeTable = table;
        }

        public List<ITable> GetDataFromTable()
        {
            return _activeTable.GetAllData(_connection);
        }

        public void AddNewRecordToTable(string[] recordParams, string fileName)
        {
            _activeTable.AddNewRecord(_connection, recordParams, ConvertToBytes(fileName));
        }

        public ITable GetRecordFromTableById(int id)
        {
            return _activeTable.GetRecordById(_connection, id);
        }

        public byte[] ConvertToBytes(string fileName)
        {
            if (fileName != null)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(fileName);
                return bytes;
            }
            else
            {
                return null;
            }
        }
        
        public void Initialization() { }
    }
}

