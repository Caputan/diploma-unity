using UnityEngine;
using System.Data.Linq;
using Mono.Data.Sqlite;
using System.IO;

public class DataBaseManager
{
    private string _dbName = "MachinePartsDB.bytes";
    private static string _dbPath;
    private static SqliteConnection _connection;
    private static DataContext _context;

    private ITable _activeTable;
    
    
    // Start is called before the first frame update
    public DataBaseManager()
    {
        _dbPath = Path.Combine(Application.streamingAssetsPath, _dbName);
        _connection = new SqliteConnection("Data Source=" + _dbPath);
        _context = new DataContext(_connection);
    }

    public void SetTable(ITable table)
    {
        this._activeTable = table;
    }

    public void GetDataFromTable()
    {
        _activeTable.GetAllData(_context);
    }
}

