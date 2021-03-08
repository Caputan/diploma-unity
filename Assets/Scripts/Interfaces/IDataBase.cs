﻿using System.Collections.Generic;
using System.Data.Linq;
using SQLite4Unity3d;

namespace Diploma.Interfaces
{
    public interface IDataBase
    {
        List<T> GetAllData<T>(SQLiteConnection connection);
        ITable GetRecordById(SQLiteConnection connection, int id);
        void AddNewRecord(SQLiteConnection connection, string[] recordParams, byte[] arrayForFiles);
    }
}
