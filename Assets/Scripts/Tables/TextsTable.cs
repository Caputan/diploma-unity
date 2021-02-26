using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using IDataBase = Interfaces.IDataBase;
using ITable = Interfaces.ITable;

namespace Tables
{
    public class TextsTable : IDataBase
    {
        public List<ITable> GetAllData(DataContext context)
        {
            Table<Texts> texts = context.GetTable<Texts>();

            var query = from text in texts select text;
        
            List<ITable> textsList = new List<ITable>();
            foreach (var text in query)
            {
                textsList.Add(text);
            }

            return textsList;
        }
    
        public ITable GetRecordById(DataContext context, int id)
        {
            Table<Texts> texts = context.GetTable<Texts>();

            var query = from text in texts where text.Text_Id == id select text;

            foreach (var text in query)
            {
                return text;
            }

            return null;
        }

        public void AddNewRecord(DataContext context, string[] textParams)
        {
            Table<Texts> texts = context.GetTable<Texts>();

            Texts newText = new Texts()
            {
                Text_Link = textParams[0]
            };
        
            texts.InsertOnSubmit(newText);
            context.SubmitChanges();
        }


        [Table(Name = "Texts")]
        public class Texts : ITable
        {
            [Column(Name = "Text_Id")] 
            public int Text_Id { get; set; }
            [Column(Name = "Text_Link")] 
            public string Text_Link { get; set; }
        }
    }
}
