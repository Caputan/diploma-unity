using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using Diploma.Interfaces;
using Mono.Data.Sqlite;
using Diploma.Tables;
using UnityEngine;
using ITable = Diploma.Interfaces.ITable;

namespace Controllers
{
    public class DataBaseController : IInitialization
    {
        private const string _dbName = "MachinePartsDB.bytes";
        private static DataContext _context;

        private IDataBase _activeTable;
    
    
        // Start is called before the first frame update
        public DataBaseController()
        {
            var dbPath = Path.Combine(Application.streamingAssetsPath, _dbName);
            var connection = new SqliteConnection("Data Source=" + dbPath);
            _context = new DataContext(connection);
        }

        public void SetTable(IDataBase table)
        {
            this._activeTable = table;
        }

        public List<ITable> GetDataFromTable()
        {
            return _activeTable.GetAllData(_context);
        }

        public void AddNewRecordToTable(string[] recordParams)
        {
            _activeTable.AddNewRecord(_context, recordParams);
        }

        public ITable GetRecordFromTableById(int id)
        {
            return _activeTable.GetRecordById(_context, id);
        }

        public void Initialization() { }
    }
}

