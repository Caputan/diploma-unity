﻿using UnityEngine;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Mono.Data.Sqlite;
using System.IO;

public interface ITable
{
    void GetAllData(DataContext context);
    void AddData(DataContext context);
    void GetRecord(DataContext context);
}
