using System.Data.Linq;
using System.IO;
using Diploma.Interfaces;
using Mono.Data.Sqlite;
using UnityEngine;

namespace Controllers
{
    public class DataBaseController : IInitialization
    {
        private const string _dbName = "MachinePartsDB.bytes";
        private static DataContext _context;

        private ITable _activeTable;
    
    
        // Start is called before the first frame update
        public DataBaseController()
        {
            var dbPath = Path.Combine(Application.streamingAssetsPath, _dbName);
            var connection = new SqliteConnection("Data Source=" + dbPath);
            _context = new DataContext(connection);
        }

        public void SetTable(ITable table)
        {
            this._activeTable = table;
        }

        public void GetDataFromTable()
        {
            _activeTable.GetAllData(_context);
        }

        public void AddNewRecordToTable(string[] recordParams)
        {
            _activeTable.AddNewRecord(_context, recordParams);
        }

        public void Initialization() { }
    }
}

