using System.Collections.Generic;
using System.Data.Linq;

namespace Diploma.Interfaces
{
    public interface IDataBase
    {
        List<ITable> GetAllData(DataContext context);
        ITable GetRecordById(DataContext context, int id);
        void AddNewRecord(DataContext context, string[] recordParams);
    }
}
